using Migree.Core.Definitions;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using Migree.Core.Models.Language;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Migree.Core.Servants
{
    public class UserServant : IUserServant
    {
        private const int PROFILE_IMAGE_SIZE_PIXELS = 400;
        private IDataRepository DataRepository { get; }
        private IContentRepository ContentRepository { get; }
        private IPasswordServant PasswordServant { get; }
        private IMailRepository MailServant { get; }
        private ILanguageServant LanguageServant { get; }
        public UserServant(IDataRepository dataRepository, IPasswordServant passwordServant, IContentRepository contentRepository, IMailRepository mailServant, ILanguageServant languageServant)
        {
            DataRepository = dataRepository;
            PasswordServant = passwordServant;
            ContentRepository = contentRepository;
            MailServant = mailServant;
            LanguageServant = languageServant;
        }

        public IUser FindUser(string email, string password)
        {
            email = email.ToLower().Trim();
            password = password.Trim();

            var user = DataRepository.GetAll<User>().FirstOrDefault(p => p.Email.Equals(email));

            if (user == null || !PasswordServant.ValidatePassword(password, user.Password))
            {
                ThrowError(Error.InvalidCredentials);
            }

            return user;
        }

        public IUser GetUser(Guid userId)
        {
            var user = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(userId))).FirstOrDefault();
            return user;
        }

        public async Task<IUser> RegisterAsync(string email, string password, string firstName, string lastName, UserType userType, BusinessGroup businessGroup)
        {
            email = email.ToLower();

            if (DataRepository.GetAll<User>().Any(p => p.Email.Equals(email)))
            {
                ThrowError(Error.UserAlreadyExist);
            }

            var user = new User(userType);
            user.Email = email;
            user.Password = PasswordServant.CreatePasswordHash(password);
            user.FirstName = firstName;
            user.LastName = lastName;            
            user.UserLocation = UserLocation.Unspecified;
            user.Description = string.Empty;
            user.HasProfileImage = false;
            user.BusinessGroup = businessGroup;
            DataRepository.AddOrUpdate(user);

            await MailServant.SendRegisterMailAsync(email, user.FullName);

            return user;
        }

        public void UpdateUser(Guid userId, string firstName, string lastName, UserType? userType, UserLocation? userLocation, string description, bool? isPublic, BusinessGroup? businessGroup)
        {
            var user = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(userId))).FirstOrDefault();

            if (userType.HasValue && user.UserType != userType.Value)
            {
                var userToReplace = user;
                user = User.GetCopy(userToReplace, userType.Value);
                DataRepository.Delete(userToReplace);
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                user.FirstName = firstName;
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                user.LastName = lastName;
            }            

            if (userLocation.HasValue)
            {
                user.UserLocation = userLocation.Value;
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                user.Description = description;
            }

            if (isPublic.HasValue)
            {
                user.IsPublic = isPublic.Value;
            }

            if (businessGroup.HasValue)
            {
                user.BusinessGroup = businessGroup.Value;
            }
            
            DataRepository.AddOrUpdate(user);
        }

        public async Task UploadProfileImageAsync(Guid userId, Stream imageStream)
        {
            var user = DataRepository.GetAll<User>(x => x.RowKey.Equals(User.GetRowKey(userId))).First();

            imageStream.Position = 0;

            using (var img = System.Drawing.Image.FromStream(imageStream))
            {
                using (var bitmap = new System.Drawing.Bitmap(img, PROFILE_IMAGE_SIZE_PIXELS, PROFILE_IMAGE_SIZE_PIXELS))
                {
                    using (var profileImageStream = new MemoryStream())
                    {
                        bitmap.Save(profileImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        await ContentRepository.PutImageAsync(userId, profileImageStream, ImageType.Profile);
                    }
                }
            }

            user.HasProfileImage = true;
            DataRepository.AddOrUpdate(user);
        }

        public string GetProfileImageUrl(Guid userId, bool hasProfileImage)
        {
            return ContentRepository.GetImageUrl(hasProfileImage ? userId : default(Guid?), ImageType.Profile);
        }

        public async Task InitPasswordResetAsync(string email)
        {
            email = email.ToLower();
            var user = DataRepository.GetAll<User>().FirstOrDefault(p => p.Email.Equals(email));

            if (user != null)
            {
                user.PasswordResetVerificationKey = DateTime.UtcNow.Ticks;
                DataRepository.AddOrUpdate(user);
                await MailServant.SendInitPasswordResetAsync(email, user.Id, user.PasswordResetVerificationKey);
            }
        }

        public async Task ResetPasswordAsync(Guid userId, string resetVerificationKey, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ThrowError(Error.PasswordEmpty);
            }

            var resetTime = Convert.ToInt64(resetVerificationKey);

            if (new DateTime(resetTime).AddHours(3) < DateTime.UtcNow)
            {
                ThrowError(Error.PasswordOld);
            }

            var user = DataRepository.GetAll<User>(p =>
                p.RowKey.Equals(User.GetRowKey(userId)) &&
                p.PasswordResetVerificationKey.Equals(resetTime)).FirstOrDefault();

            if (user == null)
            {
                ThrowError(Error.InvalidRequest);
            }

            user.PasswordResetVerificationKey = 0;
            user.Password = PasswordServant.CreatePasswordHash(newPassword);
            DataRepository.AddOrUpdate(user);
            await MailServant.SendFinishedPasswordResetAsync(user.Email);
        }        

        public IUser GetAdminUser()
        {
            var user = DataRepository.GetAll<User>(p => p.Email == "hello@migree.se").FirstOrDefault();
            return user;
        }

        private void ThrowError(Error error)
        {
            var language = LanguageServant.Get<ErrorMessages>();

            switch (error)
            {
                case Error.InvalidCredentials:
                    throw new ValidationException(HttpStatusCode.Unauthorized, language.UserInvalidCredentials);
                case Error.InvalidRequest:
                    throw new ValidationException(HttpStatusCode.BadRequest, language.UserInvalidRequest);
                case Error.PasswordEmpty:
                    throw new ValidationException(HttpStatusCode.BadRequest, language.UserPasswordEmpty);
                case Error.PasswordOld:
                    throw new ValidationException(HttpStatusCode.BadRequest, language.UserPasswordOld);
                case Error.UserAlreadyExist:
                    throw new ValidationException(HttpStatusCode.Conflict, language.UserAlreadyExist);
            }
        }

        private enum Error { InvalidRequest, PasswordOld, PasswordEmpty, InvalidCredentials, UserAlreadyExist };
    }
}

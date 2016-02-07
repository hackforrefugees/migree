using Migree.Core.Definitions;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
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
        public UserServant(IDataRepository dataRepository, IPasswordServant passwordServant, IContentRepository contentRepository, IMailRepository mailServant)
        {
            DataRepository = dataRepository;
            PasswordServant = passwordServant;
            ContentRepository = contentRepository;
            MailServant = mailServant;
        }

        public IUser FindUser(string email, string password)
        {
            email = email.ToLower();
            var user = DataRepository.GetAll<User>().FirstOrDefault(p => p.Email.Equals(email));

            if (user == null || !PasswordServant.ValidatePassword(password, user.Password))
            {
                throw new ValidationException(HttpStatusCode.Unauthorized, "Invalid credentials");
            }

            return user;
        }

        public IUser GetUser(Guid userId)
        {
            var user = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(userId))).FirstOrDefault();
            return user;
        }

        public async Task<IUser> RegisterAsync(string email, string password, string firstName, string lastName, UserType userType)
        {
            email = email.ToLower();

            if (DataRepository.GetAll<User>().Any(p => p.Email.Equals(email)))
            {
                throw new ValidationException(HttpStatusCode.Conflict, "user already exists");
            }

            var user = new User(userType);
            user.Email = email;
            user.Password = PasswordServant.CreatePasswordHash(password);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserType = userType;
            user.UserLocation = UserLocation.Unspecified;
            user.Description = string.Empty;
            user.HasProfileImage = false;
            DataRepository.AddOrUpdate(user);

            await MailServant.SendRegisterMailAsync(email, user.FullName);

            return user;
        }

        public void UpdateUser(Guid userId, UserLocation userLocation, string description)
        {
            var user = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(userId))).FirstOrDefault();
            user.UserLocation = userLocation;
            user.Description = description;
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
                throw new ValidationException(HttpStatusCode.BadRequest, "Password can´t be empty");
            }

            var resetTime = Convert.ToInt64(resetVerificationKey);

            if (new DateTime(resetTime).AddHours(3) < DateTime.UtcNow)
            {
                throw new ValidationException(HttpStatusCode.BadRequest, "Reset message to old");
            }

            var user = DataRepository.GetAll<User>(p =>
                p.RowKey.Equals(User.GetRowKey(userId)) &&
                p.PasswordResetVerificationKey.Equals(resetTime)).FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException(HttpStatusCode.BadRequest, "invalid request");
            }

            user.PasswordResetVerificationKey = 0;
            user.Password = PasswordServant.CreatePasswordHash(newPassword);
            DataRepository.AddOrUpdate(user);
            await MailServant.SendFinishedPasswordResetAsync(user.Email);
        }
    }
}

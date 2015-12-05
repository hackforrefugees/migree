using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Migree.Core.Servants
{
    public class UserServant : IUserServant
    {
        private IDataRepository DataRepository { get; }
        public UserServant(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }

        public bool ValidateUser(string email, string password)
        {
            return true;
        }

        public IUser Register(string email, string password, string firstName, string lastName, UserType userType)
        {
            return new User(userType)
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserType = userType
            };
                        
            var user = new User(userType);            
            user.Email = email;
            user.PasswordSalt = DateTime.UtcNow.Ticks.ToString();
            user.Password = EncodePassword(password, user.PasswordSalt);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserType = userType;
            DataRepository.AddOrUpdate(user);
            return user;
        }

        private string EncodePassword(string password, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
            var destinationBytes = new byte[saltBytes.Length + bytes.Length];
            Buffer.BlockCopy(saltBytes, 0, destinationBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(bytes, 0, destinationBytes, saltBytes.Length, bytes.Length);
            var algorithm = HashAlgorithm.Create("SHA1");
            var inArray = algorithm.ComputeHash(destinationBytes);
            return Convert.ToBase64String(inArray);
        }       
    }
}

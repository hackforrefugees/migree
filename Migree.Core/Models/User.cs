using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class User : StorageModel, IUser
    {
        public static string GetPartitionKey(UserType userType)
        {
            return ((int)userType).ToString();
        }

        public static string GetRowKey(Guid userId)
        {
            return userId.ToString();
        }

        public static User GetCopy(User oldUser, UserType userType)
        {
            var newUser = new User(userType);
            newUser.Id = oldUser.Id;
            newUser.Email = oldUser.Email;
            newUser.Password = oldUser.Password;
            newUser.FirstName = oldUser.FirstName;
            newUser.LastName = oldUser.LastName;
            newUser.Description = oldUser.Description;
            newUser.HasProfileImage = oldUser.HasProfileImage;
            newUser.UserLocation = oldUser.UserLocation;
            newUser.PasswordResetVerificationKey = oldUser.PasswordResetVerificationKey;
            newUser.IsPublic = oldUser.IsPublic;
            return newUser;
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public User()
        {
            IsPublic = true;
        }

        /// <summary>
        /// Create new user constructor
        /// </summary>
        /// <param name="userType"></param>
        public User(UserType userType)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey(userType);
            IsPublic = true;
        }

        [IgnoreProperty]
        public Guid Id
        {
            get { return new Guid(RowKey); }
            private set { RowKey = value.ToString(); }
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public bool HasProfileImage { get; set; }
        public bool IsPublic { get; set; }
        public UserType UserType
        {
            get { return (UserType)(Convert.ToInt32(PartitionKey)); }
            private set { PartitionKey = ((int)value).ToString(); }
        }

        public int UserLocationValue { get; set; }
        public long PasswordResetVerificationKey { get; set; }

        [IgnoreProperty]
        public UserLocation UserLocation
        {
            get { return (UserLocation)UserLocationValue; }
            set { UserLocationValue = (int)value; }
        }

        [IgnoreProperty]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}

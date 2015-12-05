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
            return userType.ToString().ToLower();
        }

        public static string GetRowKey(Guid userId)
        {
            return userId.ToString();
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public User() { }

        /// <summary>
        /// Create new user constructor
        /// </summary>
        /// <param name="userType"></param>
        public User(UserType userType)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey(userType);
        }

        [IgnoreProperty]
        public Guid Id
        {
            get
            {
                return new Guid(RowKey);
            }
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public UserType UserType { get; set; }
        public UserLocation UserLocation { get; set; }

        [IgnoreProperty]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}

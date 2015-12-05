using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class User : StorageModel, IUser
    {        
        public User()
        {
            RowKey = Guid.NewGuid().ToString();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public UserType UserType { get; set; }
    }
}

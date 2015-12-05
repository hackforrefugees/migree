using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Core.Models
{
    public class User : StorageModel, IUser
    {        
        public string Email { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }
    }
}

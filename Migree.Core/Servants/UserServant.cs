using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Core.Servants
{
    public class UserServant : IUserServant
    {
        private IDataRepository DataRepository { get; }
        public UserServant(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }

        public void Register(string email, string password, string firstName, string lastName)
        {

        }
    }
}

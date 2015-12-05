using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;

namespace Migree.Core.Interfaces
{
    public interface IUserServant
    {
        bool ValidateUser(string email, string password);
        IUser Register(string email, string password, string firstName, string lastName, UserType userType);
    }
}

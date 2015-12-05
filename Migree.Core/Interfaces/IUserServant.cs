using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface IUserServant
    {
        bool ValidateUser(string email, string password);
        IUser Register(string email, string password, string firstName, string lastName, UserType userType);
        void AddCompetencesToUser(Guid userId, ICollection<Guid> competenceIds);
        ICollection<ICompetence> GetUserCompetences(Guid userId);
    }
}

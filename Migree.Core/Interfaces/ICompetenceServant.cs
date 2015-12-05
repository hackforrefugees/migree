using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface ICompetenceServant
    {
        ICollection<ICompetence> GetCompetences();
        Guid AddCompetence(string name);
        ICollection<IUser> GetMatches(Guid userToMatchId, ICollection<Guid> competenceIds, int take);
    }
}

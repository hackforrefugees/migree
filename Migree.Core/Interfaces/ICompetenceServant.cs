using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface ICompetenceServant
    {
        /// <summary>
        /// Gets all existing comptences 
        /// </summary>
        /// <returns></returns>
        ICollection<ICompetence> GetCompetences();

        /// <summary>
        /// adding competence 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Guid AddCompetence(string name);

        /// <summary>
        /// Based on competenceIds matches will be made with other user and sorted by relevance
        /// </summary>
        /// <param name="userToMatchId">The user with competences</param>
        /// <param name="competenceIds">userToMatch competences</param>
        /// <param name="take">Number of users to take</param>
        /// <returns></returns>
        ICollection<IUser> GetMatches(Guid userToMatchId, ICollection<Guid> competenceIds, int take);

        void AddCompetencesToUser(Guid userId, ICollection<Guid> competenceIds);

        ICollection<ICompetence> GetUserCompetences(Guid userId);

    }
}

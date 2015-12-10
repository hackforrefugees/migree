using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Servants
{
    public class CompetenceServant : ICompetenceServant
    {
        private IDataRepository DataRepository { get; }
        public CompetenceServant(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        public ICollection<ICompetence> GetCompetences()
        {
            var competences = DataRepository.GetAll<Competence>(p => p.PartitionKey.Equals(Competence.GetPartitionKey(BusinessGroup.Developers)));
            return competences.OrderBy(p => p.Name).ToList<ICompetence>();
        }
        public Guid AddCompetence(string name)
        {
            var competence = new Competence(Definitions.BusinessGroup.Developers)
            {
                Name = name
            };

            DataRepository.AddOrUpdate(competence);

            return competence.Id;
        }
        public ICollection<IUser> GetMatches(Guid userToMatchId, ICollection<Guid> competenceIds, int take)
        {
            var userToMatch = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(userToMatchId))).First();
            var matchedUsers = new Dictionary<Guid, MatchedUser>();
            var users = DataRepository.GetAll<User>();
            int competenceCount = 1;

            foreach (var competenceId in competenceIds)
            {
                var usersWithCompetence = DataRepository.GetAll<UserCompetence>(p => p.PartitionKey.Equals(UserCompetence.GetPartitionKey(competenceId)));

                foreach (var userWithCompetence in usersWithCompetence)
                {
                    if (!matchedUsers.ContainsKey(userWithCompetence.UserId))
                    {
                        var user = users.FirstOrDefault(p => p.Id.Equals(userWithCompetence.UserId));

                        //ignore user self and all users within the same usertype
                        if (user == null || user.Id.Equals(userToMatchId) || user.UserType.Equals(userToMatch.UserType))
                        {
                            continue;
                        }

                        matchedUsers.Add(userWithCompetence.UserId, new MatchedUser
                        {
                            UserId = userWithCompetence.UserId,
                            User = user,
                            IsOnSameLocation = user.UserLocation.Equals(userToMatch.UserLocation)
                        });
                    }

                    if (competenceCount == 1)
                    {
                        matchedUsers[userWithCompetence.UserId].HasPrimaryCompetence = true;
                    }
                    else if (competenceCount == 2)
                    {
                        matchedUsers[userWithCompetence.UserId].HasSecondaryCompetence = true;
                    }
                    else if (competenceCount == 3)
                    {
                        matchedUsers[userWithCompetence.UserId].HasThirdCompetence = true;
                    }
                }

                competenceCount++;
            }

            var matchedUsersList = matchedUsers.Select(p => p.Value).ToList();
            matchedUsersList.Sort();
            return matchedUsersList.Select(p => p.User).Take(take).ToList();
        }

        public void AddCompetencesToUser(Guid userId, ICollection<Guid> competenceIds)
        {
            var oldCompetences = DataRepository.GetAll<UserCompetence>(p => p.RowKey.Equals(UserCompetence.GetRowKey(userId)));

            foreach (var oldCompetence in oldCompetences)
            {
                DataRepository.Delete(oldCompetence);
            }

            foreach (var competenceId in competenceIds)
            {
                var userCompetence = new UserCompetence(userId, competenceId);
                DataRepository.AddOrUpdate(userCompetence);
            }
        }

        public ICollection<ICompetence> GetUserCompetences(Guid userId)
        {
            var competences = GetCompetences();
            var userCompetences = DataRepository.GetAll<UserCompetence>(p => p.RowKey.Equals(UserCompetence.GetRowKey(userId)));
            return userCompetences.Select(p => new IdAndName { Id = p.CompetenceId, Name = competences.First(q => q.Id.Equals(p.CompetenceId)).Name }).ToList<ICompetence>();
        }
    }
}

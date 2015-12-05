using Migree.Core.Definitions;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Migree.Core.Servants
{
    public class UserServant : IUserServant
    {
        private IDataRepository DataRepository { get; }
        private IPasswordServant PasswordServant { get; }
        private ICompetenceServant CompetenceServant { get; }
        public UserServant(IDataRepository dataRepository, IPasswordServant passwordServant, ICompetenceServant competenceServant)
        {
            DataRepository = dataRepository;
            PasswordServant = passwordServant;
            CompetenceServant = competenceServant;
        }

        public IUser FindUser(string email, string password)
        {
            email = email.ToLower();
            var user = DataRepository.GetAll<User>().FirstOrDefault(p => p.Email.Equals(email));

            if (user == null || !PasswordServant.ValidatePassword(password, user.Password))
            {
                throw new ValidationException("Invalid credentials");
            }

            return user;
        }

        public IUser Register(string email, string password, string firstName, string lastName, UserType userType)
        {
            email = email.ToLower();
            var user = new User(userType);
            user.Email = email;
            user.Password = PasswordServant.CreateHash(password);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserType = userType;
            DataRepository.AddOrUpdate(user);
            return user;
        }

        public void AddCompetencesToUser(Guid userId, ICollection<Guid> competenceIds)
        {
            var rowKey = userId.ToString();
            var oldCompetences = DataRepository.GetAllByRowKey<UserCompetence>(rowKey);

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
            var competences = CompetenceServant.GetCompetences();
            var userCompetences = DataRepository.GetAllByRowKey<UserCompetence>(UserCompetence.GetRowKey(userId));
            return userCompetences.Select(p => new IdAndName { Id = p.CompetenceId, Name = competences.First(q => q.Id.Equals(p.CompetenceId)).Name }).ToList<ICompetence>();
        }
    }
}

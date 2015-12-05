using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IUserServant
    {
        IUser FindUser(string email, string password);
        IUser Register(string email, string password, string firstName, string lastName, UserType userType);
        void AddCompetencesToUser(Guid userId, ICollection<Guid> competenceIds);
        ICollection<ICompetence> GetUserCompetences(Guid userId);
        Task UploadProfileImageAsync(Guid userId, Stream imageStream);
        void UpdateUser(Guid userId, UserLocation userLocation, string description);
        Task SendMessageToUserAsync(Guid fromUserId, Guid toUserId, string message);
    }
}

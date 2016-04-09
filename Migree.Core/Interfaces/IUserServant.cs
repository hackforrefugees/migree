using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IUserServant
    {
        IUser FindUser(string email, string password);
        IUser GetUser(Guid userId);
        Task<IUser> RegisterAsync(string email, string password, string firstName, string lastName, UserType userType, BusinessGroup businessGroup);
        Task UploadProfileImageAsync(Guid userId, Stream imageStream);
        void UpdateUser(Guid userId, string firstName, string lastName, UserType? userType, UserLocation? userLocation, string description, bool? isPublic, BusinessGroup? businessGroup);
        string GetProfileImageUrl(Guid userId, bool hasProfileImage, long userLastUpdated);
        Task InitPasswordResetAsync(string email);
        Task ResetPasswordAsync(Guid userId, string resetVerificationKey, string newPassword);
        IUser GetAdminUser();
    }
}

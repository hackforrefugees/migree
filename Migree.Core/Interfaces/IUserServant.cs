﻿using Migree.Core.Definitions;
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
        Task<IUser> RegisterAsync(string email, string password, string firstName, string lastName, UserType userType);
        Task UploadProfileImageAsync(Guid userId, Stream imageStream);
        void UpdateUser(Guid userId, UserLocation userLocation, string description);        
        string GetProfileImageUrl(Guid userId);
        Task InitPasswordResetAsync(string email);
        Task ResetPasswordAsync(Guid userId, string resetVerificationKey, string newPassword);
    }
}
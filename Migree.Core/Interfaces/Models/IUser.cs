using Migree.Core.Definitions;
using System;

namespace Migree.Core.Interfaces.Models
{
    public interface IUser
    {
        Guid Id { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        string Description { get; }
        bool HasProfileImage { get; }
        long LastUpdated { get; }
        bool IsPublic { get; }
        UserType UserType { get; }
        UserLocation UserLocation { get; }
        BusinessGroup BusinessGroup { get; }
    }
}
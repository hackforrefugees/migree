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
        UserType UserType { get; }
        UserLocation UserLocation { get; }
    }
}
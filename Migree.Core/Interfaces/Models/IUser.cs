using System;

namespace Migree.Core.Interfaces.Models
{
    public interface IUser
    {
        Guid Id { get; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}

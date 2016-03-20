using System;

namespace Migree.Core.Interfaces.Models
{
    public interface IMessage
    {
        Guid Id { get; }
        Guid UserId { get; }
        string Content { get; }
        long Created { get; }
    }
}

using System;

namespace Migree.Core.Interfaces.Models
{
    public interface IMessage
    {
        Guid Id { get; }
        string Content { get; }
        long Created { get; }
    }
}

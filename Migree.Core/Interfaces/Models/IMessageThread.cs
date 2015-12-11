using System;

namespace Migree.Core.Interfaces.Models
{
    public interface IMessageThread
    {
        string MessageThreadId { get; }
        Guid UserId1 { get; }
        Guid UserId2 { get; }
        long LatestReadUser1 { get; }
        long LatestReadUser2 { get; }
        long LatestMessageCreated { get; }
        string LatestMessageContent { get; }
    }
}

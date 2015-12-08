using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces.Models
{
    public interface IMessageThread
    {
        Guid UserId1 { get; }
        Guid UserId2 { get; }
        long LatestReadUser1 { get; }
        long LatestReadUser2 { get; }
        long LatestMessageCreated { get; }
        string LatestMessageContent { get; }
    }
}

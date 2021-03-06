﻿using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces
{
    public interface IMessageServant
    {
        Task SendMessageToUserAsync(Guid creatorUserId, Guid receiverUserId, string message);
        ICollection<KeyValuePair<IMessageThread, IUser>> GetMessageThreads(Guid userId);
        KeyValuePair<IUser, ICollection<IMessage>> GetMessageThread(Guid currentUserId, Guid otherUserId);
        void SetMessageThreadAsRead(Guid currentUserId, Guid otherUserId);
    }
}

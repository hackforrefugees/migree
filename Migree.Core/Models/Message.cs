using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class Message : StorageModel, IMessage
    {
        public static string GetPartitionKey(Guid userId1, Guid userId2)
        {
            return MessageThread.GetRowKey(userId1, userId2);
        }

        public static string GetRowKey(Guid messageId)
        {
            return messageId.ToString();
        }

        /// <summary>
        /// Used by azure
        /// </summary>
        public Message() { }

        public Message(Guid creatorUserId, Guid receiverUserId)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey(creatorUserId, receiverUserId);            
        }

        [IgnoreProperty]
        public Guid Id
        {
            get
            {
                return new Guid(RowKey);
            }
        }

        public string Content { get; set; }

        public long Created { get; set; }
    }
}

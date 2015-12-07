using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Migree.Core.Models
{
    public class Message : StorageModel
    {
        public static string GetPartitionKey(Guid senderUserId)
        {
            return senderUserId.ToString();
        }

        public static string GetRowKey(Guid id)
        {
            return id.ToString();            
        }

        /// <summary>
        /// Used by azure
        /// </summary>
        public Message() { }

        public Message(Guid senderUserId)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = senderUserId.ToString();
            Sent = DateTime.UtcNow.Ticks;            
        }

        [IgnoreProperty]
        public Guid Id
        {
            get
            {
                return new Guid(RowKey);
            }
        }

        [IgnoreProperty]
        public Guid SenderUserId
        {
            get
            {
                return new Guid(PartitionKey);
            }
        }

        public Guid ReceiverUserId { get; set; }

        public string Content { get; set; }

        public long Sent { get; set; }
    }
}

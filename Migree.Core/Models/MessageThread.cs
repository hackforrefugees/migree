using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Models
{
    public class MessageThread : StorageModel
    {
        public static string GetPartitionKey(Guid userId1, Guid userId2)
        {
            var firstCharacterInGuids = SortedIds(userId1, userId2).Select(p => p.ToString().Remove(1));
            return string.Join("", firstCharacterInGuids);
        }

        public static string GetRowKey(Guid userId1, Guid userId2)
        {
            return string.Join("_", SortedIds(userId1, userId2));
        }

        private static List<Guid> SortedIds(Guid userId1, Guid userId2)
        {
            var userIds = new List<Guid>
            {
                userId1,
                userId2
            };

            return userIds.OrderBy(p => p).ToList();
        }

        /// <summary>
        /// Used by Azure
        /// </summary>
        public MessageThread() { }

        public MessageThread(Guid creatorUserId, Guid receiverUserId)
        {
            RowKey = GetRowKey(creatorUserId, receiverUserId);
            PartitionKey = GetPartitionKey(creatorUserId, receiverUserId);
            LatestReadUser1 = 0;
            LatestReadUser2 = 0;
        }

        [IgnoreProperty]
        public Guid UserId1 { get { return GetUserIdByIndex(0); } }

        [IgnoreProperty]
        public Guid UserId2 { get { return GetUserIdByIndex(1); } }

        public long LatestMessageCreated { get; set; }
        public long LatestReadUser1 { get; set; }
        public long LatestReadUser2 { get; set; }

        private Guid GetUserIdByIndex(int index)
        {
            var splittedKey = RowKey.Split(new char[] { '_' });
            return new Guid(splittedKey[index]);
        }
    }
}

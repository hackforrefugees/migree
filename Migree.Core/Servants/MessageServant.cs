using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Migree.Core.Servants
{
    public class MessageServant : IMessageServant
    {
        private IDataRepository DataRepository { get; }
        private IMailRepository MailServant { get; }

        public MessageServant(IDataRepository dataRepository, IMailRepository mailServant)
        {
            DataRepository = dataRepository;
            MailServant = mailServant;
        }

        public async Task SendMessageToUserAsync(Guid creatorUserId, Guid receiverUserId, string message)
        {
            AddMessage(creatorUserId, receiverUserId, message);
            await MailServant.SendMessageMailAsync(creatorUserId, receiverUserId, message);
        }

        public ICollection<KeyValuePair<IMessageThread, IUser>> GetMessageThreads(Guid userId)
        {
            var messageThreadsWithUser = new List<KeyValuePair<IMessageThread, IUser>>();

            var messageThreads = DataRepository
                .GetAll<MessageThread>()
                .Where(p => p.RowKey.Contains(userId.ToString()))
                .OrderByDescending(p => p.LatestMessageCreated);

            var userIdsAsRowKeys = messageThreads.Select(p => User.GetRowKey(p.UserId1)).ToList();
            userIdsAsRowKeys.AddRange(messageThreads.Select(p => User.GetRowKey(p.UserId2)));
            userIdsAsRowKeys = userIdsAsRowKeys.Distinct().ToList();

            var usersInThreads = DataRepository.GetAll<User>().Where(p => userIdsAsRowKeys.Contains(p.RowKey)).ToList();

            foreach (var messageThread in messageThreads)
            {
                var userIdToGet = (messageThread.UserId1.Equals(userId) ? messageThread.UserId2 : messageThread.UserId1);
                var user = usersInThreads.FirstOrDefault(p => p.Id.Equals(userIdToGet));

                if (user == null)
                {
                    continue;
                }

                messageThreadsWithUser.Add(new KeyValuePair<IMessageThread, IUser>(messageThread, user));
            }

            return messageThreadsWithUser;
        }

        public KeyValuePair<IUser, ICollection<IMessage>> GetMessageThread(Guid currentUserId, Guid otherUserId)
        {
            var messagesInThread = DataRepository
                .GetAll<Message>(p => p.PartitionKey.Equals(Message.GetPartitionKey(currentUserId, otherUserId)))
                .OrderByDescending(p => p.Created).ToList<IMessage>();

            var otherUser = DataRepository.GetAll<User>(p => p.RowKey.Equals(User.GetRowKey(otherUserId))).FirstOrDefault();

            if (otherUser == null || messagesInThread.Count == 0)
            {
                throw new ValidationException(System.Net.HttpStatusCode.NotFound, "thread doesn´t exist");
            }

            return new KeyValuePair<IUser, ICollection<IMessage>>(otherUser, messagesInThread);
        }

        public void SetMessageThreadAsRead(Guid currentUserId, Guid otherUserId)
        {
            var thread = DataRepository.GetFirstOrDefault<MessageThread>(MessageThread.GetPartitionKey(currentUserId, otherUserId), MessageThread.GetRowKey(currentUserId, otherUserId));

            if (thread.UserId1.Equals(currentUserId))
            {
                thread.LatestReadUser1 = DateTime.UtcNow.Ticks;
            }
            else if (thread.UserId2.Equals(currentUserId))
            {
                thread.LatestReadUser2 = DateTime.UtcNow.Ticks;
            }

            DataRepository.AddOrUpdate(thread);
        }

        private void AddMessage(Guid creatorUserId, Guid receiverUserId, string content)
        {
            var messageThread = DataRepository.GetFirstOrDefault<MessageThread>(
                MessageThread.GetPartitionKey(creatorUserId, receiverUserId),
                MessageThread.GetRowKey(creatorUserId, receiverUserId));

            var messageTimestamp = DateTime.UtcNow.Ticks;

            if (messageThread == null)
            {
                messageThread = new MessageThread(creatorUserId, receiverUserId);
                messageThread.LatestReadUser1 = messageThread.UserId1.Equals(creatorUserId) ? messageTimestamp : 0;
                messageThread.LatestReadUser2 = messageThread.UserId2.Equals(creatorUserId) ? messageTimestamp : 0;
            }

            var message = new Message(creatorUserId, receiverUserId)
            {
                Content = content,
                Created = messageTimestamp
            };

            messageThread.LatestMessageContent = content;
            messageThread.LatestMessageCreated = messageTimestamp;

            DataRepository.AddOrUpdate(message);
            DataRepository.AddOrUpdate(messageThread);
        }
    }
}

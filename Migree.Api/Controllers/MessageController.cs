using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("message")]
    public class MessageController : MigreeApiController
    {
        private IMessageServant MessageServant { get; }
        private IUserServant UserServant { get; }
        private ILanguageServant LanguageServant { get; }
        public MessageController(IMessageServant messageServant, IUserServant userServant, ILanguageServant languageServant)
        {
            MessageServant = messageServant;
            UserServant = userServant;
            LanguageServant = languageServant;
        }

        [HttpPost, Route("")]
        public async Task<HttpResponseMessage> PostMessageAsync(PostMessageRequest request)
        {
            if (request.ReceiverUserId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ValidationException(HttpStatusCode.BadRequest, LanguageServant.Get<ErrorMessages>().MessageRequiredFieldsMissing);
            }

            await MessageServant.SendMessageToUserAsync(CurrentUserId, request.ReceiverUserId, request.Message);
            return CreateApiResponse(HttpStatusCode.Accepted);
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetMessageThreads()
        {
            var messageThreads = MessageServant.GetMessageThreads(CurrentUserId);
            var response = messageThreads.Select(p => new MessageThreadResponse
            {
                OtherUserId = p.Value.Id,
                FullName = $"{p.Value.FirstName} {p.Value.LastName}",
                ProfileImageUrl = UserServant.GetProfileImageUrl(p.Value.Id, p.Value.HasProfileImage),
                IsRead = p.Key.LatestMessageCreated < (p.Key.UserId1.Equals(CurrentUserId) ? p.Key.LatestReadUser1 : p.Key.LatestReadUser2),
                LatestMessageContent = p.Key.LatestMessageContent,
                LastUpdated = p.Key.LatestMessageCreated.ToRelativeDateTimeString(LanguageServant.Get<RelativeDateTimeStrings>())
            });
            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpGet, Route("{otherUserId:guid}")]
        public HttpResponseMessage GetMessageThread(Guid otherUserId)
        {
            var messagesInThreadWithUser = MessageServant.GetMessageThread(CurrentUserId, otherUserId);

            if (messagesInThreadWithUser.Value.Count > 0)
            {
                MessageServant.SetMessageThreadAsRead(CurrentUserId, otherUserId);
            }

            var message = new MessageResponse
            {
                User = new MessageResponse.UserItem
                {
                    FullName = $"{messagesInThreadWithUser.Key.FirstName} {messagesInThreadWithUser.Key.LastName}",
                    UserLocation = LanguageServant.Get<Definition>().UserLocation[messagesInThreadWithUser.Key.UserLocation.ToString()],
                    ProfileImageUrl = UserServant.GetProfileImageUrl(messagesInThreadWithUser.Key.Id, messagesInThreadWithUser.Key.HasProfileImage)
                },
                Messages = messagesInThreadWithUser.Value.Select(p => new MessageResponse.MessageItem
                {
                    Content = p.Content,
                    Created = p.Created.ToRelativeDateTimeString(LanguageServant.Get<RelativeDateTimeStrings>()),
                    IsCurrentUser = p.UserId.Equals(CurrentUserId)
                }).ToList()
            };

            return CreateApiResponse(HttpStatusCode.OK, message);
        }
    }
}
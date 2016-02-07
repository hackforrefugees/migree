using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
                throw new ValidationException(HttpStatusCode.BadRequest, "Requried fields missing");
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
                ProfileImageUrl = UserServant.GetProfileImageUrl(p.Value.Id),
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
            MessageServant.SetMessageThreadAsRead(CurrentUserId, otherUserId);
            var user = messagesInThreadWithUser.Key;

            var messagesInThread = messagesInThreadWithUser.Value.Select(p => new MessageResponse
            {
                Content = p.Content,
                Created = p.Created.ToRelativeDateTimeString(LanguageServant.Get<RelativeDateTimeStrings>())
            });

            return CreateApiResponse(HttpStatusCode.OK, messagesInThread);
        }
    }
}
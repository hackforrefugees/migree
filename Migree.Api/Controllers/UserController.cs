using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : MigreeApiController
    {
        private const int NUMBER_OF_MATCHES_TO_TAKE = 50;

        private IUserServant UserServant { get; }
        private ICompetenceServant CompetenceServant { get; }
        private IMessageServant MessageServant { get; }

        public UserController(IUserServant userServant, ICompetenceServant comptenceServant, IMessageServant messageServant)
        {
            UserServant = userServant;
            CompetenceServant = comptenceServant;
            MessageServant = messageServant;
        }

        [HttpGet, Route("{userId:guid}/competences")]
        public HttpResponseMessage GetUserCompetences(Guid userId)
        {
            try
            {
                var competences = CompetenceServant.GetUserCompetences(userId);
                var response = competences.Select(x => new GuidIdAndNameResponse { Id = x.Id, Name = x.Name }).ToList();
                return CreateApiResponse(HttpStatusCode.OK, response);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost, Route("")]
        public async Task<HttpResponseMessage> RegisterAsync(RegisterRequest request)
        {
            try
            {
                if (
                    string.IsNullOrEmpty(request.Email) ||
                    string.IsNullOrEmpty(request.Password) ||
                    string.IsNullOrEmpty(request.FirstName) ||
                    string.IsNullOrEmpty(request.LastName)
                    )
                {
                    return CreateApiResponse(HttpStatusCode.BadRequest);
                }

                var user = await UserServant.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName, request.UserType);
                return CreateApiResponse(HttpStatusCode.OK, new RegisterResponse { UserId = user.Id });
            }
            catch (ValidationException)
            {
                return CreateApiResponse(HttpStatusCode.Conflict);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet, Route("{userId:guid}")]
        public HttpResponseMessage GetUser(Guid userId)
        {
            try
            {
                var user = UserServant.GetUser(userId);
                return CreateApiResponse(HttpStatusCode.OK, GetUserResponse(user));
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost, Route("{userId:guid}/upload")]
        public async Task<HttpResponseMessage> UploadProfileImageAsync(Guid userId)
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return CreateApiResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var content = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
                using (var imageStream = await content.Contents.First().ReadAsStreamAsync())
                {
                    await UserServant.UploadProfileImageAsync(userId, imageStream);
                }

                return CreateApiResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet, Route("{userId:guid}/matches")]
        public HttpResponseMessage GetMatches(Guid userId)
        {
            try
            {
                var userMatches = CompetenceServant.GetUserCompetences(userId).Select(p => p.Id).ToList();
                var matchedUsers = CompetenceServant.GetMatches(userId, userMatches, NUMBER_OF_MATCHES_TO_TAKE);
                var users = matchedUsers.Select(user => GetUserResponse(user)).ToList();

                return CreateApiResponse(HttpStatusCode.OK, users);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut, Route("{userId:guid}")]
        public HttpResponseMessage Update(Guid userId, UpdateUserRequest request)
        {
            try
            {
                if (request.CompetenceIds?.Count < 1)
                {
                    return CreateApiResponse(HttpStatusCode.BadRequest);
                }

                UserServant.UpdateUser(userId, request.UserLocation, request.Description ?? string.Empty);
                CompetenceServant.AddCompetencesToUser(userId, request.CompetenceIds);
                return CreateApiResponse(HttpStatusCode.NoContent);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost, Route("{userId:guid}/message")]
        public async Task<HttpResponseMessage> PostMessageAsync(Guid userId, PostMessageRequest request)
        {
            try
            {
                if (request.ReceiverUserId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(request.Message))
                {
                    return CreateApiResponse(HttpStatusCode.BadRequest);
                }

                await MessageServant.SendMessageToUserAsync(userId, request.ReceiverUserId, request.Message);
                return CreateApiResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet, Route("{userId:guid}/messages")]
        public HttpResponseMessage GetMessageThreads(Guid userId)
        {
            var messageThreads = MessageServant.GetMessageThreads(userId);
            var response = messageThreads.Select(p => new MessageThreadResponse
            {
                UserId = p.Value.Id,
                FullName = $"{p.Value.FirstName} {p.Value.LastName}",
                ProfileImageUrl = UserServant.GetProfileImageUrl(p.Value.Id),
                IsRead = p.Key.LatestMessageCreated < (p.Key.UserId1.Equals(userId) ? p.Key.LatestReadUser1 : p.Key.LatestReadUser2),
                LatestMessageContent = p.Key.LatestMessageContent,
                LastUpdated = ToRelativeDateTimeString(p.Key.LatestMessageCreated)
            }).ToList();
            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        private UserResponse GetUserResponse(IUser user)
        {
            var response = new UserResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Description = user.Description,
                UserLocation = user.UserLocation.ToDescription(),
                ProfileImageUrl = UserServant.GetProfileImageUrl(user.Id),
                Competences = CompetenceServant.GetUserCompetences(user.Id).Select(x => new GuidIdAndNameResponse { Id = x.Id, Name = x.Name }).ToList()
            };

            return response;
        }

        private string ToRelativeDateTimeString(long timestamp)
        {
            var timeDifference = DateTime.UtcNow - new DateTime(timestamp);

            if (timeDifference.Days > 1)
            {
                return $"{timeDifference.Days} days ago";
            }
            else if (timeDifference.Days == 1)
            {
                return "1 day ago";
            }
            else if (timeDifference.Hours > 1)
            {
                return $"{timeDifference.Days} hours ago";
            }
            else if (timeDifference.Hours == 1)
            {
                return "1 hour ago";
            }
            else if (timeDifference.Minutes > 5)
            {
                return $"{timeDifference.Minutes} hours ago";
            }
            else
            {
                return "a moment ago";
            }
        }
    }
}

using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
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
        private IUserServant UserServant { get; }
        private ICompetenceServant CompetenceServant { get; }
        private ILanguageServant LanguageServant { get; }

        public UserController(IUserServant userServant, ICompetenceServant comptenceServant, ILanguageServant languageServant)
        {
            UserServant = userServant;
            CompetenceServant = comptenceServant;
            LanguageServant = languageServant;
        }

        [HttpPost, Route(""), AllowAnonymous]
        public async Task<HttpResponseMessage> RegisterAsync(RegisterRequest request)
        {
            if (
                !request.Email.IsValidEmail() ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName)
                )
            {
                throw new ValidationException(HttpStatusCode.BadRequest, LanguageServant.Get<ErrorMessages>().UserInvalidRequest);
            }

            var user = await UserServant.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName, request.UserType);
            return CreateApiResponse(HttpStatusCode.OK, new RegisterResponse { UserId = user.Id });
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetUser()
        {
            var user = UserServant.GetUser(CurrentUserId);

            var response = new UserDetailedResponse
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserType = user.UserType,
                Description = user.Description,
                UserLocation = user.UserLocation,
                HasProfileImage = user.HasProfileImage,
                IsPublic = user.IsPublic,
                ProfileImageUrl = UserServant.GetProfileImageUrl(user.Id, user.HasProfileImage),
                Competences = CompetenceServant.GetUserCompetences(user.Id).Select(x => x.Id).ToList(),                
            };

            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpPost, Route("upload")]
        public async Task<HttpResponseMessage> UploadProfileImageAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new ValidationException(HttpStatusCode.UnsupportedMediaType, LanguageServant.Get<ErrorMessages>().UserInvalidRequest);
            }

            var content = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
            using (var imageStream = await content.Contents.First().ReadAsStreamAsync())
            {
                await UserServant.UploadProfileImageAsync(CurrentUserId, imageStream);
            }

            return CreateApiResponse(HttpStatusCode.Accepted);
        }

        [HttpPut, Route("")]
        public HttpResponseMessage Update(UpdateUserRequest request)
        {
            UserServant.UpdateUser(CurrentUserId, request.FirstName, request.LastName, request.UserType, request.UserLocation, request.Description, request.IsPublic);

            if (request.CompetenceIds?.Count > 0)
            {
                CompetenceServant.AddCompetencesToUser(CurrentUserId, request.CompetenceIds);
            }

            return CreateApiResponse(HttpStatusCode.NoContent);
        }
    }
}

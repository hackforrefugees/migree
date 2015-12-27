using Migree.Api.Models.Requests;
using Migree.Api.Models.Responses;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
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

        public UserController(IUserServant userServant, ICompetenceServant comptenceServant)
        {
            UserServant = userServant;
            CompetenceServant = comptenceServant;            
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
                throw new ValidationException(HttpStatusCode.BadRequest, "Required fields missing");
            }

            var user = await UserServant.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName, request.UserType);
            return CreateApiResponse(HttpStatusCode.OK, new RegisterResponse { UserId = user.Id });
        }

        [HttpGet, Route("")]
        public HttpResponseMessage GetUser()
        {
            var user = UserServant.GetUser(CurrentUserId);

            var response = new UserResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Description = user.Description,
                UserLocation = user.UserLocation.ToDescription(),
                ProfileImageUrl = UserServant.GetProfileImageUrl(user.Id),
                Competences = CompetenceServant.GetUserCompetences(user.Id).Select(x => new GuidIdAndNameResponse { Id = x.Id, Name = x.Name }).ToList()
            };            

            return CreateApiResponse(HttpStatusCode.OK, response);
        }

        [HttpPost, Route("upload")]
        public async Task<HttpResponseMessage> UploadProfileImageAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new ValidationException(HttpStatusCode.UnsupportedMediaType, "MimeType is not correct");                
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
            if (request.CompetenceIds?.Count < 1)
            {
                throw new ValidationException(HttpStatusCode.BadRequest, "Requried fields missing");                
            }

            UserServant.UpdateUser(CurrentUserId, request.UserLocation, request.Description ?? string.Empty);
            CompetenceServant.AddCompetencesToUser(CurrentUserId, request.CompetenceIds);
            return CreateApiResponse(HttpStatusCode.NoContent);
        }                
    }
}

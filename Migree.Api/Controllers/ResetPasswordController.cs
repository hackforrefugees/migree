using Migree.Api.Models.Requests;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("resetpassword")]
    public class ResetPasswordController : MigreeApiController
    {
        private IUserServant UserServant { get; }
        public ResetPasswordController(IUserServant userServant)
        {
            UserServant = userServant;
        }

        [HttpPost, Route(""), AllowAnonymous]
        public async Task<HttpResponseMessage> InitPasswordResetAsync(InitPasswordResetRequest request)
        {
            await UserServant.InitPasswordResetAsync(request.Email);
            return CreateApiResponse(HttpStatusCode.Accepted);
        }

        [HttpPut, Route(""), AllowAnonymous]
        public async Task<HttpResponseMessage> PasswordResetAsync(PasswordResetRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                throw new ValidationException(HttpStatusCode.BadRequest, "Requried fields missing");
            }

            await UserServant.ResetPasswordAsync(request.UserId, request.ResetVerificationKey, request.NewPassword);
            return CreateApiResponse(HttpStatusCode.Accepted);
        }
    }
}
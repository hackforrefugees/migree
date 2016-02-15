using Migree.Api.Models.Requests;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
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
        private ILanguageServant LanguageServant { get; }
        public ResetPasswordController(IUserServant userServant, ILanguageServant languageServant)
        {
            UserServant = userServant;
            LanguageServant = languageServant;
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
                throw new ValidationException(HttpStatusCode.BadRequest, LanguageServant.Get<ErrorMessages>().ResetPasswordRequiredFieldsMissing);
            }

            await UserServant.ResetPasswordAsync(request.UserId, request.ResetVerificationKey, request.NewPassword);
            return CreateApiResponse(HttpStatusCode.Accepted);
        }
    }
}
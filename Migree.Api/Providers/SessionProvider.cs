using Migree.Api.Configuration;
using Migree.Core.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Migree.Api.Providers
{
    public class SessionProvider : ISessionProvider
    {
        public Guid CurrentUserId
        {
            get
            {
                try
                {
                    var request = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                    var requestContext = request.GetRequestContext();
                    var principal = requestContext.Principal as System.Security.Claims.ClaimsPrincipal;
                    var userIdClaim = principal.Claims.FirstOrDefault(p => p.Type == Global.ClaimUserId)?.Value ?? null;

                    if (!string.IsNullOrEmpty(userIdClaim))
                        return Guid.Parse(userIdClaim);
                }
                catch { }

                throw new ValidationException(HttpStatusCode.BadRequest, "User not found");
            }
        }
    }
}
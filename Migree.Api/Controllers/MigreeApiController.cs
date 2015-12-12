using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Migree.Core.Exceptions;

namespace Migree.Api.Controllers
{
    [Authorize]
    public abstract class MigreeApiController : ApiController
    {
        protected HttpResponseMessage CreateApiResponse(HttpStatusCode statusCode, object content = null, Uri locationUrl = null)
        {
            var response = ControllerContext.Request.CreateResponse(statusCode, content);

            if (locationUrl != null)
                response.Headers.Location = locationUrl;

            return response;
        }

        protected Uri GetLocationUri(string attributeRouteName, object route = null)
        {
            string uri = Url.Link(attributeRouteName, route);
            return new Uri(uri);
        }

        protected Guid CurrentUserId
        {
            get
            {
                try
                {
                    var requestContext = Request.GetRequestContext();
                    var principal = requestContext.Principal as System.Security.Claims.ClaimsPrincipal;
                    var userIdClaim = principal.Claims.FirstOrDefault(p => p.Type == "userId")?.Value ?? null;

                    if (!string.IsNullOrEmpty(userIdClaim))
                        return Guid.Parse(userIdClaim);
                }
                catch { }

                throw new ValidationException("User id doesn´t exist");
            }
        }
    }
}
using Migree.Api.Configuration;
using Migree.Core.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [Authorize]
    public abstract class MigreeApiController : ApiController
    {
        protected HttpResponseMessage CreateApiResponse(HttpStatusCode statusCode, object content = null, Uri locationUrl = null)
        {
            var response = ControllerContext.Request.CreateResponse(statusCode, content);

            if (locationUrl != null)
            {
                response.Headers.Location = locationUrl;
            }

            return response;
        }

        protected Uri GetLocationUri(string attributeRouteName, object route = null)
        {
            string uri = Url.Link(attributeRouteName, route);
            return new Uri(uri);
        }        
    }
}

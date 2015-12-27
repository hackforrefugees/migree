using Migree.Api.Models.Responses;
using Migree.Core.Definitions;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("location")]
    public class LocationController : MigreeApiController
    {
        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            var business = Enum.GetValues(typeof(UserLocation)).Cast<UserLocation>().OrderBy(p => (int)p == 0).ThenBy(p => p.ToDescription()).Select(p => new IntIdAndNameResponse
            {
                Id = (int)p,
                Name = p.ToDescription()
            });

            return CreateApiResponse(System.Net.HttpStatusCode.OK, business);
        }
    }
}
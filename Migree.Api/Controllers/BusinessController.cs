using Migree.Api.Models.Responses;
using Migree.Core.Definitions;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("business")]
    public class BusinessController : MigreeApiController
    {
        [HttpGet, Route("")]
        public HttpResponseMessage GetAll()
        {
            var business = Enum.GetValues(typeof(BusinessGroup)).Cast<BusinessGroup>().ToList().Select(p => new IntIdAndNameResponse
            {
                Id = (int)p,
                Name = p.ToDescription()
            });

            return CreateApiResponse(System.Net.HttpStatusCode.OK, business);
        }
    }
}
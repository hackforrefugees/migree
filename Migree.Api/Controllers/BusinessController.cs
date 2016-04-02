using Migree.Api.Models;
using Migree.Core.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("business")]
    public class BusinessController : MigreeApiController
    {
        private IBusinessServant BusinessServant { get; }

        public BusinessController(IBusinessServant businessServant)
        {
            BusinessServant = businessServant;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            var business = BusinessServant.GetAll();
            var response = business.Select(x => new IntIdAndName { Id = x.Id, Name = x.Name });
            return CreateApiResponse(System.Net.HttpStatusCode.OK, response);
        }
    }
}
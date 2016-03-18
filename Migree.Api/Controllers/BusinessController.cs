using Migree.Api.Models;
using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("business")]
    public class BusinessController : MigreeApiController
    {
        private ILanguageServant LanguageServant { get; }

        public BusinessController(ILanguageServant languageServant)
        {
            LanguageServant = languageServant;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            var language = LanguageServant.Get<Definition>().Business;

            var business = Enum.GetValues(typeof(BusinessGroup))
                .Cast<BusinessGroup>()
                .OrderBy(p => language[p.ToString()])
                .Select(p => new IntIdAndName
                {
                    Id = (int)p,
                    Name = language[p.ToString()]
                });

            return CreateApiResponse(System.Net.HttpStatusCode.OK, business);
        }
    }
}
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
    [RoutePrefix("location")]
    public class LocationController : MigreeApiController
    {
        private ILanguageServant LanguageServant { get; }

        public LocationController(ILanguageServant languageServant)
        {
            LanguageServant = languageServant;
        }

        [HttpGet, Route(""), AllowAnonymous]
        public HttpResponseMessage GetAll()
        {
            var language = LanguageServant.Get<Definition>().UserLocation;
            
            var locations = Enum.GetValues(typeof(UserLocation))
                .Cast<UserLocation>()
                .OrderBy(p => (int)p == 0)
                .ThenBy(p => language[p.ToString()])
                .Select(p => new IntIdAndName
                {
                    Id = (int)p,
                    Name = language[p.ToString()]
                });

            return CreateApiResponse(System.Net.HttpStatusCode.OK, locations);
        }
    }
}
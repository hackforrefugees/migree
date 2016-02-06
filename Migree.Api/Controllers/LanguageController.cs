using Migree.Api.Models.Responses;
using Migree.Core.Interfaces;
using Migree.Core.Models.Language;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers
{
    [RoutePrefix("language")]
    public class LanguageController : MigreeApiController
    {
        private ILanguageServant LanguageServant { get; }
        public LanguageController(ILanguageServant languageServant)
        {
            LanguageServant = languageServant;
        }

        [HttpGet, Route("{languageCode:regex(^[a-z]{2}$)}"), AllowAnonymous]
        public HttpResponseMessage Get(string languageCode)
        {
            var dictionary = LanguageServant.GetDictionary();
            return CreateApiResponse(HttpStatusCode.OK, dictionary);
        }
    }
}
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

        [HttpGet, Route("{languageCode:regex(^[a-z]{2}$)}")]
        public HttpResponseMessage Get(string languageCode)
        {
            try
            {
                var dictionary = LanguageServant.GetDictionary<Client>();
                
                return CreateApiResponse(HttpStatusCode.OK, dictionary.Select(p => new StringIdAndNameResponse
                {
                    Id = p.Key,
                    Name = p.Value
                }));
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
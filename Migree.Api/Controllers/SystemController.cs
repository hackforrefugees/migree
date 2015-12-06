using Migree.Api.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Api.Controllers.Api
{
    [RoutePrefix("system")]
    public class SystemController : MigreeApiController
    {
        [HttpGet]
        [Route("language")]
        public virtual HttpResponseMessage Language()
        {
            try
            {
                var dictionary = new LanguageDictionary();
                dictionary.Add("login", new LanguageDictionaryItem { en = "Login", sv = "Logga in" });
                dictionary.Add("password", new LanguageDictionaryItem { en = "Password", sv = "Lösenord" });
                return CreateApiResponse(HttpStatusCode.OK, dictionary);
            }
            catch
            {
                return CreateApiResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}

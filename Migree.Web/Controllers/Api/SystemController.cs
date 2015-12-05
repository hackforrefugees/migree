using Migree.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Migree.Web.Controllers.Api
{
    [RoutePrefix("api/system")]
    public class SystemController : MigreeApiController
    {        
        [HttpGet]
        [Route("language")]
        public virtual HttpResponseMessage Language()
        {
            var dictionary = new LanguageDictionary();
            dictionary.Add("login", new DictionaryItem { en = "Login", sv = "Logga in" });
            dictionary.Add("password", new DictionaryItem { en = "Password", sv = "Lösenord" });
            return CreateApiResponse(HttpStatusCode.OK, dictionary);
        }
    }
}

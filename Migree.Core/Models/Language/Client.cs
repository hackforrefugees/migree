using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class Client : ILanguage
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

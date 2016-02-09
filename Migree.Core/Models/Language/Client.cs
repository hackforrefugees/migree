using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Core.Models.Language
{
    public class Client : ILanguage, IClient
    {
        [JsonProperty("home")]
        public IDictionary<string, string> Home { get; set; }
        [JsonProperty("login")]
        public IDictionary<string, string> Login { get; set; }
        [JsonProperty("resetPassword")]
        public IDictionary<string, string> ResetPassword { get; set; }
        [JsonProperty("finishPasswordReset")]
        public IDictionary<string, string> FinishPasswordReset { get; set; }
        [JsonProperty("notFound")]
        public IDictionary<string, string> NotFound { get; set; }
        [JsonProperty("thankYou")]
        public IDictionary<string, string> ThankYou { get; set; }
        [JsonProperty("message")]
        public IDictionary<string, string> Message { get; set; }
        [JsonProperty("messages")]
        public IDictionary<string, string> Messages { get; set; }
        [JsonProperty("register")]
        public IDictionary<string, string> Register { get; set; }
        [JsonProperty("matches")]
        public IDictionary<string, string> Matches { get; set; }
    }
}

using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public abstract class LanguageMail
    {
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("fromName")]
        public string FromName { get; set; }
        [JsonProperty("fromMail")]
        public string FromMail { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

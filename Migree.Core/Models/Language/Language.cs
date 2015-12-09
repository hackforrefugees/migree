using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class Language
    {
        [JsonProperty("sendMessageMail")]
        public SendMessageMail SendMessageMail { get; set; }
    }
}

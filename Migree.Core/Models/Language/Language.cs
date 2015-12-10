using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class Language
    {
        [JsonProperty("sendMessageMail")]
        public SendMessageMail SendMessageMail { get; set; }
        [JsonProperty("sendRegistrationMail")]
        public SendRegistrationMail SendRegistrationMail { get; set; }
        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}

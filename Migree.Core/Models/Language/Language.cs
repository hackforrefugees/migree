using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class Language
    {
        [JsonProperty("messageMail")]
        public MessageMail MessageMail { get; set; }
        [JsonProperty("registrationMail")]
        public RegistrationMail RegistrationMail { get; set; }
        [JsonProperty("initPasswordResetMail")]
        public InitPasswordResetMail InitPasswordResetMail { get; set; }
        [JsonProperty("finishedPasswordResetMail")]
        public FinishedPasswordResetMail FinishedPasswordResetMail { get; set; }
        [JsonProperty("client")]
        public Client Client { get; set; }
    }
}

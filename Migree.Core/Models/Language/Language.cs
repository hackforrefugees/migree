﻿using Newtonsoft.Json;

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
        [JsonProperty("definition")]
        public Definition Definition { get; set; }
        [JsonProperty("relativeDateTimeStrings")]
        public RelativeDateTimeStrings RelativeDateTimeStrings { get; set; }
        [JsonProperty("errorMessages")]
        public ErrorMessages ErrorMessages { get; set; }
    }
}

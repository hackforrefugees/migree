using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class ErrorMessages : ILanguage
    {
        [JsonProperty("messageRequiredFieldsMissing")]
        public string MessageRequiredFieldsMissing { get; set; }
        [JsonProperty("resetPasswordRequiredFieldsMissing")]
        public string ResetPasswordRequiredFieldsMissing { get; set; }
        [JsonProperty("currentUserNotFound")]
        public string CurrentUserNotFound { get; set; }
        [JsonProperty("invalidGrant")]
        public string InvalidGrant { get; set; }
        [JsonProperty("messageThreadNotFound")]
        public string MessageThreadNotFound { get; set; }
        [JsonProperty("userInvalidCredentials")]
        public string UserInvalidCredentials { get; set; }
        [JsonProperty("userInvalidRequest")]
        public string UserInvalidRequest { get; set; }
        [JsonProperty("userPasswordEmpty")]
        public string UserPasswordEmpty { get; set; }
        [JsonProperty("userPasswordOld")]
        public string UserPasswordOld { get; set; }
        [JsonProperty("userAlreadyExist")]
        public string UserAlreadyExist { get; set; }
    }
}

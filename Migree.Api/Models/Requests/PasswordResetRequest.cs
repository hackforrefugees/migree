using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Requests
{
    public class PasswordResetRequest
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        [JsonProperty("resetKey")]
        public string ResetVerificationKey { get; set; }
        [JsonProperty("password")]
        public string NewPassword { get; set; }
    }
}
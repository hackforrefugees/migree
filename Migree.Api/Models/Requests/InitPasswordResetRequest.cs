using Newtonsoft.Json;

namespace Migree.Api.Models.Requests
{
    public class InitPasswordResetRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
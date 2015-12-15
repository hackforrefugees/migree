using Newtonsoft.Json;

namespace Migree.Api.Models.Responses
{
    public class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
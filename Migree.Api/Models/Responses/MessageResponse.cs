using Newtonsoft.Json;

namespace Migree.Api.Models.Responses
{
    public class MessageResponse
    {        
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
    }
}
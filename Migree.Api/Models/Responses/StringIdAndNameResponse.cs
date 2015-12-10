using Newtonsoft.Json;

namespace Migree.Api.Models.Responses
{
    public class StringIdAndNameResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
using Newtonsoft.Json;

namespace Migree.Api.Models.Responses
{
    public class StringIdAndNameResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
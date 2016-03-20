using Newtonsoft.Json;

namespace Migree.Api.Models
{
    public class StringIdAndName
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
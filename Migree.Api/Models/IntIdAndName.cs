using Newtonsoft.Json;

namespace Migree.Api.Models
{
    public class IntIdAndName
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
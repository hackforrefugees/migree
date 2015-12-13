using Newtonsoft.Json;

namespace Migree.Api.Models.Responses
{
    public class IntIdAndNameResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
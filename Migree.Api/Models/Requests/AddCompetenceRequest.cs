using Newtonsoft.Json;

namespace Migree.Api.Models.Requests
{
    public class AddCompetenceRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
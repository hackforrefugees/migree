using Newtonsoft.Json;

namespace Migree.Web.Models.Requests
{
    public class AddCompetenceRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
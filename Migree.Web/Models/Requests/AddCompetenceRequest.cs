using Newtonsoft.Json;

namespace Migree.Web.Models.Requests
{
    public class AddCompetenceRequest
    {
        [JsonProperty(PropertyName = "competence")]
        public string Name { get; set; }
    }
}
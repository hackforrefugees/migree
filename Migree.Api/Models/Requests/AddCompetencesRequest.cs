using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Api.Models.Requests
{
    public class AddCompetencesRequest
    {
        [JsonProperty(PropertyName = "competences")]
        public ICollection<string> Competences { get; set; }
    }
}
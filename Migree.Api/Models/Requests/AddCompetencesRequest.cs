using Migree.Core.Definitions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Api.Models.Requests
{
    public class AddCompetencesRequest
    {
        [JsonProperty("businessGroup")]
        public BusinessGroup BusinessGroup { get; set; }

        [JsonProperty("competences")]
        public ICollection<string> Competences { get; set; }
    }
}
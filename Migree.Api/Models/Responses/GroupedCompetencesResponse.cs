using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Migree.Api.Models.Responses
{
    public class GroupedCompetencesResponse : List<BusinessCompetenceResponse> { }

    public class BusinessCompetenceResponse
    {
        [JsonProperty("business")]
        public IntIdAndName Business { get; set; }
        [JsonProperty("competences")]
        public ICollection<GuidIdAndName> Competences { get; set; }
    }
}
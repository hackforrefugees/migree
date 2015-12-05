using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Web.Models.Requests
{
    public class FindMatchesRequest
    {
        [JsonProperty(PropertyName = "competenceIds")]
        public ICollection<Guid> CompetenceIds { get; set; }
    }
}
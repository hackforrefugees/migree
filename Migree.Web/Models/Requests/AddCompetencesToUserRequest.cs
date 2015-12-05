using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Web.Models.Requests
{
    public class AddCompetencesToUserRequest
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
        [JsonProperty(PropertyName = "competenceIds")]
        public ICollection<Guid> CompetenceIds { get; set; }
    }
}
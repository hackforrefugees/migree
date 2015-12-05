using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Web.Models.Requests
{
    public class UpdateUserRequest
    {
        [JsonProperty(PropertyName = "userLocation")]
        public UserLocation UserLocation { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "competenceIds")]
        public ICollection<Guid> CompetenceIds { get; set; }
    }
}
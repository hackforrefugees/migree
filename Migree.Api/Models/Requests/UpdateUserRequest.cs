using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Api.Models.Requests
{
    public class UpdateUserRequest
    {
        [JsonProperty("userLocation")]
        public UserLocation UserLocation { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("competenceIds")]
        public ICollection<Guid> CompetenceIds { get; set; }
    }
}
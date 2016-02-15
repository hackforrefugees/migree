using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Api.Models.Requests
{
    public class UpdateUserRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("userType")]
        public UserType? UserType { get; set; }
        [JsonProperty("userLocation")]
        public UserLocation? UserLocation { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("competenceIds")]
        public ICollection<Guid> CompetenceIds { get; set; }
    }
}
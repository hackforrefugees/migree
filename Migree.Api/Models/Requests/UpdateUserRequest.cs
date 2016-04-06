using Migree.Core.Definitions;
using Newtonsoft.Json;
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
        public IntIdAndName UserLocation { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("isPublic")]
        public bool? IsPublic { get; set; }
        [JsonProperty("competences")]
        public ICollection<GuidIdAndName> Competences { get; set; }
        [JsonProperty("businessGroup")]
        public BusinessGroup? BusinessGroup { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Api.Models.Responses
{
    public class UserResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        [JsonProperty("name")]
        public string FullName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("userLocation")]
        public string UserLocation { get; set; }
        [JsonProperty("profileImageUrl")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("competences")]
        public ICollection<GuidIdAndNameResponse> Competences { get; set; }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Api.Models.Responses
{
    public class UserResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "userLocation")]
        public string UserLocation { get; set; }
        [JsonProperty(PropertyName = "profileImageUrl")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty(PropertyName = "competences")]
        public ICollection<IdAndNameResponse> Competences { get; set; }
    }
}
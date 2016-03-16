using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Migree.Api.Models.Responses
{
    public class UserDetailedResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("userType")]
        public UserType UserType { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("userLocation")]
        public UserLocation UserLocation { get; set; }
        [JsonProperty("hasProfileImage")]
        public bool HasProfileImage { get; set; }
        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }
        [JsonProperty("profileImageUrl")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("competences")]
        public ICollection<Guid> Competences { get; set; }
    }
}
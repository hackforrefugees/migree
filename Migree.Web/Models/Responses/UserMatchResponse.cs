using Newtonsoft.Json;
using System;

namespace Migree.Web.Models.Responses
{
    public class UserMatchResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "userLocation")]
        public string UserLocation { get; set; }
    }
}
using Newtonsoft.Json;
using System;

namespace Migree.Web.Models.Responses
{
    public class UserMatchResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "userocation")]
        public string UserLocation { get; set; }
    }
}
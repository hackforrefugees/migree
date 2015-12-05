using Migree.Core.Definitions;
using Newtonsoft.Json;

namespace Migree.Web.Models.Requests
{
    public class UpdateUserRequest
    {
        [JsonProperty(PropertyName = "userLocation")]
        public UserLocation UserLocation { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Api.Models.Responses
{
    public class MessageResponse
    {
        [JsonProperty("user")]
        public UserItem User { get; set; }
        [JsonProperty("messages")]
        public ICollection<MessageItem> Messages { get; set; }

        public class MessageItem
        {
            [JsonProperty("content")]
            public string Content { get; set; }
            [JsonProperty("created")]
            public string Created { get; set; }
            [JsonProperty("isUser")]
            public bool IsCurrentUser { get; set; }
        }

        public class UserItem
        {
            [JsonProperty("name")]
            public string FullName { get; set; }
            [JsonProperty("userLocation")]
            public string UserLocation { get; set; }
            [JsonProperty("profileImageUrl")]
            public string ProfileImageUrl { get; set; }            
        }
    }
}
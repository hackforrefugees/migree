using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Responses
{
    public class MessageThreadResponse
    {        
        [JsonProperty("id")]
        public Guid OtherUserId { get; set; }
        [JsonProperty("name")]
        public string FullName { get; set; }
        [JsonProperty("profileImageUrl")]
        public string ProfileImageUrl { get; set; }
        [JsonProperty("content")]
        public string LatestMessageContent { get; set; }
        [JsonProperty("lastUpdate")]
        public string LastUpdated { get; set; }
        [JsonProperty("isRead")]
        public bool IsRead { get; set; }        
    }
}
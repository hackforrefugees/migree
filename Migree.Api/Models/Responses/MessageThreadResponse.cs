using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Migree.Api.Models.Responses
{
    public class MessageThreadResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "profileImageUrl")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string LatestMessageContent { get; set; }

        [JsonProperty(PropertyName = "lastUpdate")]
        public string LastUpdated { get; set; }

        [JsonProperty(PropertyName = "isRead")]
        public bool IsRead { get; set; }        
    }
}
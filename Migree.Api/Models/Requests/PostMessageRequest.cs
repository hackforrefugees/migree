using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Requests
{
    public class PostMessageRequest
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid ReceiverUserId { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Requests
{
    public class PostMessageRequest
    {
        [JsonProperty("userId")]
        public Guid ReceiverUserId { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Responses
{
    public class MessageResponse
    {
        [JsonProperty("messageId")]
        public Guid MessageId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
    }
}
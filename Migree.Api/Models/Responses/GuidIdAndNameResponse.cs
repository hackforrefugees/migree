using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Responses
{
    public class GuidIdAndNameResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

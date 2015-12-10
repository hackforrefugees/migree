using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Responses
{
    public class GuidIdAndNameResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

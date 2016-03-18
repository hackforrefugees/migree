using Newtonsoft.Json;
using System;

namespace Migree.Api.Models
{
    public class GuidIdAndName
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

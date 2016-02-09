using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Core.Models.Language
{
    public class Definition : IDefinition
    {
        [JsonProperty("business")]
        public IDictionary<string, string> Business { get; }
        [JsonProperty("userLocation")]
        public IDictionary<string, string> UserLocation { get; }
        [JsonProperty("userType")]
        public IDictionary<string, string> UserType { get; }
    }
}

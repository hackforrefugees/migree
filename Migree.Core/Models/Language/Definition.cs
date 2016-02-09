using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Migree.Core.Models.Language
{
    public class Definition : IDefinition, ILanguage
    {
        [JsonProperty("business")]
        public IDictionary<string, string> Business { get; set; }
        [JsonProperty("userLocation")]
        public IDictionary<string, string> UserLocation { get; set; }
        [JsonProperty("userType")]
        public IDictionary<string, string> UserType { get; set; }
    }
}

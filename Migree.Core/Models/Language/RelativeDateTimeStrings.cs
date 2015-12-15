using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;

namespace Migree.Core.Models.Language
{
    public class RelativeDateTimeStrings : ILanguage
    {
        [JsonProperty("days")]
        public string Days { get; set; }
        [JsonProperty("day")]
        public string Day { get; set; }
        [JsonProperty("hours")]
        public string Hours { get; set; }
        [JsonProperty("hour")]
        public string Hour { get; set; }
        [JsonProperty("minutes")]
        public string Minutes { get; set; }
        [JsonProperty("now")]
        public string Now { get; set; }
    }
}

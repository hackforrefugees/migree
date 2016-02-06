using Migree.Core.Interfaces.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Migree.Core.Models.Language
{
    public class Client : ILanguage, IClient
    {
        [JsonProperty("home")]
        public IDictionary<string, string> Home { get; set; }

        [JsonProperty("login")]
        public IDictionary<string, string> Login { get; set; }
    }
}

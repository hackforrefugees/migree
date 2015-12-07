using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Responses
{
    public class RegisterResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}

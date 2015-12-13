using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Requests
{
    public class RegisterRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("userType")]
        public UserType UserType { get; set; }        
    }
}
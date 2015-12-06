using Migree.Core.Definitions;
using Newtonsoft.Json;
using System;

namespace Migree.Api.Models.Requests
{
    public class RegisterRequest
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "userType")]
        public UserType UserType { get; set; }        
    }
}
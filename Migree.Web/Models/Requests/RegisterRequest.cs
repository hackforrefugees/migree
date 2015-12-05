﻿using Newtonsoft.Json;

namespace Migree.Web.Models.Requests
{
    public class RegisterRequest
    {
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Web.Models.Responses
{
    public class RegisterResponse
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Core.Interfaces.Models
{
    public interface IClient
    {
        IDictionary<string, string> Home { get; }
        IDictionary<string, string> Login { get; }
        IDictionary<string, string> ResetPassword { get; }
    }
}

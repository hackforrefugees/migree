using System.Collections.Generic;

namespace Migree.Core.Interfaces.Models
{
    public interface IDefinition
    {
        IDictionary<string, string> Business { get; }
        IDictionary<string, string> UserLocation { get; }
        IDictionary<string, string> UserType { get; }
    }
}

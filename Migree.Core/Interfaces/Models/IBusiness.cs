using Migree.Core.Definitions;

namespace Migree.Core.Interfaces.Models
{
    public interface IBusiness
    {
        int Id { get; }
        string Name { get; }
        BusinessGroup Type { get; }
    }
}

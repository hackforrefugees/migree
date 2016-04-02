using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;

namespace Migree.Core.Models
{
    public class Business : IBusiness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BusinessGroup Type { get; set; }
    }
}

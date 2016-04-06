using Migree.Core.Interfaces.Models;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface IBusinessServant
    {
        ICollection<IBusiness> GetAll();
    }
}

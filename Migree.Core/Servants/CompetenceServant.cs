using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Servants
{
    public class CompetenceServant : ICompetenceServant
    {
        public ICollection<ICompetence> GetCompetences()
        {
            return new List<Competence>
            {
                new Competence { Name = "C#" },
                new Competence {Name = "C" }
            }.ToList<ICompetence>();
        }
    }
}

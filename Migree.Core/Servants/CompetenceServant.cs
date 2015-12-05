using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Servants
{
    public class CompetenceServant : ICompetenceServant
    {
        private IDataRepository DataRepository { get; }
        public CompetenceServant(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        public ICollection<ICompetence> GetCompetences()
        {
            return new List<Competence>
            {
                new Competence { Name = "C#" },
                new Competence {Name = "C" }
            }.ToList<ICompetence>();

            var competences = DataRepository.GetAll<Competence>(Competence.GetPartitionKey());
            return competences.OrderBy(p => p.Name).ToList<ICompetence>();
        }

        public void AddCompetence(string name)
        {
            var competence = new Competence
            {
                Name = name
            };

            DataRepository.AddOrUpdate(competence);
        }
    }
}

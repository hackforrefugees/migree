using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using System;
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
            var competences = DataRepository.GetAll<Competence>(Competence.GetPartitionKey());
            return competences.OrderBy(p => p.Name).ToList<ICompetence>();
        }

        public Guid AddCompetence(string name)
        {
            var competence = new Competence
            {
                Name = name
            };

            DataRepository.AddOrUpdate(competence);

            return competence.Id;
        }
    }
}

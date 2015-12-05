using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class IdAndName : ICompetence
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migree.Core.Models
{
    public class UserCompetence : StorageModel
    {
        public Guid UserId { get; set; }
        public Guid CompetenceId { get; set; }
    }
}

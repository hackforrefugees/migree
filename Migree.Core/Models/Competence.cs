using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class Competence : StorageModel, ICompetence
    {
        public static string GetPartitionKey(ProfessionGroup professionGroup)
        {
            return ((int)professionGroup).ToString();
        }

        public static string GetRowKey(Guid competenceId)
        {
            return competenceId.ToString();
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public Competence() { }
        
        public Competence(ProfessionGroup professionGroup)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey(professionGroup);
        }

        [IgnoreProperty]
        public Guid Id
        {
            get
            {
                return new Guid(RowKey);
            }
        }

        public string Name { get; set; }
    }
}

using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Definitions;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class Competence : StorageModel, ICompetence
    {
        public static string GetPartitionKey(Definitions.BusinessGroup businessGroup)
        {
            return ((int)businessGroup).ToString();
        }

        public static string GetRowKey(Guid competenceId)
        {
            return competenceId.ToString();
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public Competence() { }
        
        public Competence(Definitions.BusinessGroup businessGroup)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey(businessGroup);
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

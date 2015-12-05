using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Interfaces.Models;
using System;

namespace Migree.Core.Models
{
    public class Competence : StorageModel, ICompetence
    {
        public static string GetPartitionKey()
        {
            return "systemdevelopers";
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public Competence()
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = GetPartitionKey();
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

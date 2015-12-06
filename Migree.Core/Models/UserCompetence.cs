using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Migree.Core.Models
{
    public class UserCompetence : StorageModel
    {
        public static string GetPartitionKey(Guid competenceId)
        {
            return competenceId.ToString();
        }

        public static string GetRowKey(Guid userId)
        {
            return userId.ToString();
        }

        /// <summary>
        /// Default, used by Azure
        /// </summary>
        public UserCompetence() { }

        public UserCompetence(Guid userId, Guid competenceId)
        {
            RowKey = GetRowKey(userId);
            PartitionKey = GetPartitionKey(competenceId);
        }

        [IgnoreProperty]
        public Guid UserId
        {
            get
            {
                return new Guid(RowKey);
            }
        }

        [IgnoreProperty]
        public Guid CompetenceId
        {
            get
            {
                return new Guid(PartitionKey);
            }
        }
    }
}

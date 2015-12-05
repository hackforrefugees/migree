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

        public UserCompetence() { }

        public UserCompetence(Guid userId, Guid competenceId)
        {
            RowKey = userId.ToString();
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

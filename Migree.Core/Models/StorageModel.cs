using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Migree.Core.Models
{
    public abstract class StorageModel : TableEntity
    {
        [IgnoreProperty]
        public Guid Id
        {
            get
            {
                return new Guid(RowKey);
            }
        }
    }
}

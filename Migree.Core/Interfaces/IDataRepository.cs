using Migree.Core.Models;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface IDataRepository
    {
        ICollection<Model> GetAll<Model>(string partitionKey)
            where Model : StorageModel, new();
        ICollection<Model> GetAll<Model>()
            where Model : StorageModel, new();
        Model Get<Model>(string partitionKey, string rowKey)
            where Model : StorageModel, new();
        void AddOrUpdate<Model>(Model model)
            where Model : StorageModel, new();
        void Delete<Model>(Model model)
            where Model : StorageModel, new();
        ICollection<Model> GetAllByRowKey<Model>(string rowKey)
             where Model : StorageModel, new();
    }
}

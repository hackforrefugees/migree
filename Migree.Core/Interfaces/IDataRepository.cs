using Migree.Core.Models;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface IDataRepository
    {
        /// <summary>
        /// Get all models in partition
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="partitionKey"></param>
        /// <returns></returns>
        ICollection<Model> GetAll<Model>(string partitionKey)
            where Model : StorageModel, new();

        /// <summary>
        /// Get all models
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <returns></returns>
        ICollection<Model> GetAll<Model>()
            where Model : StorageModel, new();

        /// <summary>
        /// Get first or default
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        Model GetFirstOrDefault<Model>(string partitionKey, string rowKey)
            where Model : StorageModel, new();       
        
        /// <summary>
        /// Add model if rowkey in partition exists, otherwise update
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="model"></param>                 
        void AddOrUpdate<Model>(Model model)
            where Model : StorageModel, new();

        /// <summary>
        /// Delete model with partitionkey and rowkey
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="model"></param>
        void Delete<Model>(Model model)
            where Model : StorageModel, new();

        /// <summary>
        /// Get models from several partitions
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        ICollection<Model> GetAllByRowKey<Model>(string rowKey)
             where Model : StorageModel, new();

        /// <summary>
        /// Get first model with row key
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        Model GetFirstOrDefaultByRowKey<Model>(string rowKey)
             where Model : StorageModel, new();
    }
}

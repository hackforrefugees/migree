using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Migree.Core.Interfaces
{
    public interface IDataRepository
    {
        /// <summary>
        /// Get all models satisfying where
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        ICollection<Model> GetAll<Model>(Expression<Func<Model, bool>> where = null)
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
    }
}

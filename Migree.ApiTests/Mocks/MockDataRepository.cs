using Migree.Core.Interfaces;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Migree.ApiTests.Mocks
{
    public class MockDataRepository : IDataRepository
    {
        private List<StorageModel> MockModels { get; } = new List<StorageModel>();

        public void AddOrUpdate<Model>(Model model) where Model : StorageModel, new()
        {
            var existingModel = GetFirstOrDefault<Model>(model.PartitionKey, model.RowKey);

            if (existingModel != null)
            {
                MockModels.Remove(existingModel);
            }

            MockModels.Add(model);
        }

        public void Delete<Model>(Model model) where Model : StorageModel, new()
        {
            var existingModel = GetFirstOrDefault<Model>(model.PartitionKey, model.RowKey);

            if (existingModel != null)
            {
                MockModels.Remove(existingModel);
            }
        }

        public ICollection<Model> GetAll<Model>(Expression<Func<Model, bool>> where = null) where Model : StorageModel, new()
        {
            var models = MockModels.Where(p => p.GetType().Equals(typeof(Model))).Cast<Model>();

            if (models != null)
            {
                models = models.AsQueryable().Where(where);
            }

            return models.ToList();
        }

        public Model GetFirstOrDefault<Model>(string partitionKey, string rowKey) where Model : StorageModel, new()
        {
            return GetAll<Model>(p => p.PartitionKey.Equals(partitionKey) && p.RowKey.Equals(rowKey)).FirstOrDefault();
        }
    }
}

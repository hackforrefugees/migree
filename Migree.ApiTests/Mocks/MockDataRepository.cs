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
        public MockDataRepository()
        {
            MockModels = new List<StorageModel>();
            Fill();
        }

        private List<StorageModel> MockModels { get; set; }

        public IEnumerable<Model> GetMockModels<Model>() where Model : StorageModel
        {
            return MockModels.Where(p => p.GetType().Equals(typeof(Model))).Cast<Model>();
        }

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
            var models = GetMockModels<Model>();

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

        private void Fill()
        {
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "App development" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Frontend" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Backend" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Database" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Built in system" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Copy" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Design" });
            MockModels.Add(new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Copywriter" });

            MockModels.Add(new User(Core.Definitions.UserType.Helper)
            {
                BusinessGroup = Core.Definitions.BusinessGroup.Developers,
                Description = "Description one",
                Email = "one@mail.com",
                FirstName = "First",
                LastName = "FirstLast",
                HasProfileImage = false,
                IsPublic = true,
                UserLocation = Core.Definitions.UserLocation.GothenburgArea
            });
        }
    }
}

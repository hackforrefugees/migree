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

            if (models != null && where != null)
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
            var competences = new List<Competence>
            {
                new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "App development" },
                new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Frontend" },
                new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Backend" },
                new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Database" },
                new Competence(Core.Definitions.BusinessGroup.Developers) { Name = "Built in system" },
                new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Copy" },
                new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Design" },
                new Competence(Core.Definitions.BusinessGroup.Marketing) { Name = "Copywriter" }
            };

            MockModels.AddRange(competences);

            var users = new List<User>
            {
                new User(Core.Definitions.UserType.Helper)
                {
                    BusinessGroup = Core.Definitions.BusinessGroup.Developers,
                    Description = "Description one",
                    Email = "one@mail.com",
                    FirstName = "First",
                    LastName = "FirstLast",
                    HasProfileImage = false,
                    IsPublic = true,
                    UserLocation = Core.Definitions.UserLocation.GothenburgArea
                },
                new User(Core.Definitions.UserType.NeedsHelp)
                {
                    BusinessGroup = Core.Definitions.BusinessGroup.Developers,
                    Description = "Description two",
                    Email = "two@mail.com",
                    FirstName = "Second",
                    LastName = "SecondLast",
                    HasProfileImage = false,
                    IsPublic = true,
                    UserLocation = Core.Definitions.UserLocation.GothenburgArea
                }
            };

            MockModels.AddRange(users);

            MockModels.Add(new UserCompetence(users[0].Id, competences[0].Id));
            MockModels.Add(new UserCompetence(users[0].Id, competences[1].Id));
            MockModels.Add(new UserCompetence(users[1].Id, competences[0].Id));
        }
    }
}

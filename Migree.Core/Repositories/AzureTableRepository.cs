using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Migree.Core.Repositories
{
    public class AzureTableRepository : IDataRepository
    {
        private ISettingsServant SettingsServant { get; }
        public AzureTableRepository(ISettingsServant settingsServant)
        {
            SettingsServant = settingsServant;
        }

        public ICollection<Model> GetAll<Model>(Expression<Func<Model, bool>> where = null)
            where Model : StorageModel, new()
        {
            try
            {
                var queryableResult = GetTableReference<Model>().CreateQuery<Model>();

                if (where != null)
                {
                    return queryableResult.Where(where).ToList();
                }

                return queryableResult.ToList();
            }
            catch
            {
                throw new DataModelException("Get all failed");
            }
        }

        public void Delete<Model>(Model model)
            where Model : StorageModel, new()
        {
            try
            {
                var result = GetTableReference<Model>().Execute(TableOperation.Delete(model));

                if (!result.HttpStatusCode.IsSuccess())
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new DataModelException("Deletion was unsuccesful");
            }
        }

        public Model GetFirstOrDefault<Model>(string partitionKey, string rowKey)
             where Model : StorageModel, new()
        {
            try
            {
                var queryableResult = GetTableReference<Model>()
                    .CreateQuery<Model>()
                    .Where(p => p.PartitionKey.Equals(partitionKey) && p.RowKey.Equals(rowKey));
                return queryableResult.FirstOrDefault();
            }
            catch
            {
                throw new DataModelException("Get failed");
            }
        }        

        public void AddOrUpdate<Model>(Model model)
            where Model : StorageModel, new()
        {
            try
            {
                var result = GetTableReference<Model>().Execute(TableOperation.InsertOrReplace(model));

                if (!result.HttpStatusCode.IsSuccess())
                {
                    throw new Exception();
                }
            }
            catch (MigreeException exception)
            {
                throw exception;
            }
            catch
            {
                throw new DataModelException("Add or update failed");
            }
        }

        private CloudTable GetTableReference<Model>()
        {
            var tableName = typeof(Model).Name.ToLower();
            var storageAccount = CloudStorageAccount.Parse(SettingsServant.StorageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            tableClient.DefaultRequestOptions = new TableRequestOptions(
                new TableRequestOptions
                {
                    PayloadFormat = TablePayloadFormat.JsonNoMetadata
                });

            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}

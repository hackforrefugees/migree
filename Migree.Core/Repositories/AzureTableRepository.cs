using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using Migree.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Repositories
{
    public class AzureTableRepository : IDataRepository
    {
        private ISettingsServant SettingsServant { get; }
        public AzureTableRepository(ISettingsServant settingsServant)
        {
            SettingsServant = settingsServant;
        }

        public ICollection<Model> GetAll<Model>(string partitionKey)
            where Model : StorageModel, new()
        {
            var queryableResult = GetTableReference<Model>().CreateQuery<Model>().Where(p => p.PartitionKey.Equals(partitionKey));
            return queryableResult.ToList();
        }

        public void Delete<Model>(Model model)
            where Model : StorageModel, new()
        {
            try
            {
                var result = GetTableReference<Model>().Execute(TableOperation.Delete(model));

                if (!result.HttpStatusCode.IsSuccess())
                {
                    throw new Exception("No success deleting model");
                }
            }
            catch
            {
                throw new DataModelException("Deletion was unsuccesful");
            }
        }

        public Model Get<Model>(string partitionKey, string rowKey)
             where Model : StorageModel, new()
        {
            var queryableResult = GetTableReference<Model>().CreateQuery<Model>()
                .Where(p => p.PartitionKey.Equals(partitionKey) && p.RowKey.Equals(rowKey));
            return queryableResult.FirstOrDefault();
        }

        public Guid AddOrUpdate<Model>(Model model)
            where Model : StorageModel, new()
        {
            try
            {
                var result = GetTableReference<Model>().Execute(TableOperation.InsertOrReplace(model));
                return ((StorageModel)result.Result).Id;
            }
            catch (MigreeException exception)
            {
                throw exception;
            }
            catch
            {
                throw new DataModelException($"Add or update failed for {model?.Id}");
            }
        }

        private CloudTable GetTableReference<Model>()
        {
            var tableName = nameof(Model).ToLower();
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

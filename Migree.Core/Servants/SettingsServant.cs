using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using System.Configuration;

namespace Migree.Core.Servants
{
    public class SettingsServant : ISettingsServant
    {
        private const string AZURE_STORAGE_CONNECTION_STRING = "AzureStorageConnectionString";
        public SettingsServant()
        {
            try
            {
                StorageConnectionString = ConfigurationManager.ConnectionStrings[AZURE_STORAGE_CONNECTION_STRING].ConnectionString;
            }
            catch
            {
                throw new EnvironmentException($"ConnectionStrings.config is probably missing! Project startup failed");
            }
        }

        public string StorageConnectionString { get; }
    }
}
using Migree.Core.Exceptions;
using Migree.Core.Interfaces;
using System;
using System.Configuration;
using System.Net;

namespace Migree.Core.Servants
{
    public class SettingsServant : ISettingsServant
    {
        private const string AZURE_STORAGE_CONNECTION_STRING = "AzureStorageConnectionString";
        private const string SENDGRID_CONNECTION_STRING = "SendGridConnectionString";        
        public SettingsServant()
        {
            try
            {
                StorageConnectionString = ConfigurationManager.ConnectionStrings[AZURE_STORAGE_CONNECTION_STRING].ConnectionString;
                var sendgrid = ConfigurationManager.ConnectionStrings[SENDGRID_CONNECTION_STRING].ConnectionString.Split(new char[] { '|' });
                SendGridCredentials = new NetworkCredential(sendgrid[0], sendgrid[1]);
                DataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            }
            catch
            {
                throw new EnvironmentException("ConnectionStrings.config is probably missing! Project startup failed");
            }
        }

        public string StorageConnectionString { get; }
        public NetworkCredential SendGridCredentials { get; }
        public string DataDirectory { get; }
    }
}
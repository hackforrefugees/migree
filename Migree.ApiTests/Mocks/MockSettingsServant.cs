using Migree.Core.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Migree.ApiTests.Mocks
{
    public class MockSettingsServant : ISettingsServant
    {
        public string DataDirectory
        {
            get
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                path = path.Remove(path.IndexOf("Migree.ApiTests"));
                path = Path.Combine(path, @"Migree.Api\App_Data");
                //remove file:\\
                path = path.Substring(6);
                return path;
            }
        }
        public NetworkCredential SendGridCredentials { get { throw new NotImplementedException(); } }
        public string StorageConnectionString { get { throw new NotImplementedException(); } }
    }
}

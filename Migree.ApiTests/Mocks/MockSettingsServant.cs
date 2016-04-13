using Migree.Core.Interfaces;
using System;
using System.Net;

namespace Migree.ApiTests.Mocks
{
    public class MockSettingsServant : ISettingsServant
    {
        public string DataDirectory { get { return string.Empty; } }
        public NetworkCredential SendGridCredentials { get { throw new NotImplementedException(); } }
        public string StorageConnectionString { get { throw new NotImplementedException(); } }
    }
}

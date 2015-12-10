using System.Net;

namespace Migree.Core.Interfaces
{
    public interface ISettingsServant
    {
        string StorageConnectionString { get; }
        NetworkCredential SendGridCredentials { get; }
        string DataDirectory { get; }
    }
}

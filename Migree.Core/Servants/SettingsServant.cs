using Migree.Core.Interfaces;

namespace Migree.Core.Servants
{
    public class SettingsServant : ISettingsServant
    {
        public SettingsServant()
        {
            StorageConnectionString = "";
        }

        public string StorageConnectionString { get; }
    }
}
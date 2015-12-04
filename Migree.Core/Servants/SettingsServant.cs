using Migree.Core.Interfaces;

namespace Migree.Core.Servants
{
    public class SettingsServant : ISettingsServant
    {
        public SettingsServant()
        {
            StorageConnectionString = "sdsdsdf==";
        }

        public string StorageConnectionString { get; }
    }
}

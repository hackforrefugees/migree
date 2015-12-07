using System.ComponentModel;

namespace Migree.Core.Definitions
{
    public enum UserType
    {
        [Description("Need help")]
        NeedsHelp = 1,
        [Description("Want to help")]
        Helper = 2
    }
}

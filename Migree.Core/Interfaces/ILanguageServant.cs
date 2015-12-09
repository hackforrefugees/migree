using Migree.Core.Interfaces.Models;

namespace Migree.Core.Interfaces
{
    public interface ILanguageServant
    {
        LanguageItem Get<LanguageItem>()
            where LanguageItem : class, ILanguage;
        string Get(string languageString, params string[] args);
    }
}

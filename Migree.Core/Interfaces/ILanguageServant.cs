using Migree.Core.Interfaces.Models;

namespace Migree.Core.Interfaces
{
    public interface ILanguageServant
    {
        LanguageItem Get<LanguageItem>()
            where LanguageItem : class, ILanguage;
        string GetString(string languageString, params object[] args);
        IClient GetDictionary();
    }
}

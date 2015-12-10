using Migree.Core.Interfaces.Models;
using System.Collections.Generic;

namespace Migree.Core.Interfaces
{
    public interface ILanguageServant
    {
        LanguageItem Get<LanguageItem>()
            where LanguageItem : class, ILanguage;
        string Get(string languageString, params string[] args);
        IDictionary<string, string> GetDictionary<LanguageItem>()
            where LanguageItem : class, ILanguage;
    }
}

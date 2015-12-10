using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models.Language;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Migree.Core.Servants
{
    public class LanguageServant : ILanguageServant
    {
        private const string ENGLISH_LANGUAGE_FILENAME = "lang-en.json";

        private ISettingsServant SettingsServant { get; }

        public LanguageServant(ISettingsServant settingServant)
        {
            SettingsServant = settingServant;
        }

        public LanguageItem Get<LanguageItem>()
            where LanguageItem : class, ILanguage
        {
            var languageJson = File.ReadAllText(Path.Combine(SettingsServant.DataDirectory, ENGLISH_LANGUAGE_FILENAME));
            var language = JsonConvert.DeserializeObject<Language>(languageJson);

            if (typeof(LanguageItem).Name == nameof(MessageMail))
            {
                return language.MessageMail as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(RegistrationMail))
            {
                return language.RegistrationMail as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(InitPasswordResetMail))
            {
                return language.InitPasswordResetMail as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(FinishedPasswordResetMail))
            {
                return language.FinishedPasswordResetMail as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(Client))
            {
                return language.Client as LanguageItem;
            }

            return default(LanguageItem);
        }

        public string Get(string languageString, params object[] args)
        {
            try
            {
                return string.Format(languageString, args);
            }
            catch
            {
                return string.Empty;
            }
        }

        public IDictionary<string, string> GetDictionary<LanguageItem>()
            where LanguageItem : class, ILanguage
        {
            var language = Get<LanguageItem>();

            var dictionary = language
                .GetType().
                GetProperties().
                ToDictionary(p => p.Name, p => p.GetValue(language) as string);

            return dictionary;
        }
    }
}

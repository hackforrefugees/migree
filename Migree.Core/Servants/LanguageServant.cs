using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models.Language;
using Newtonsoft.Json;
using System.IO;

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
            else if (typeof(LanguageItem).Name == nameof(Definition))
            {
                return language.Definition as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(RelativeDateTimeStrings))
            {
                return language.RelativeDateTimeStrings as LanguageItem;
            }
            else if (typeof(LanguageItem).Name == nameof(ErrorMessages))
            {
                return language.ErrorMessages as LanguageItem;
            }

            return default(LanguageItem);
        }

        public string GetString(string languageString, params object[] args)
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

        public IClient GetDictionary()
        {
            var language = Get<Client>();
            return language;
        }
    }
}

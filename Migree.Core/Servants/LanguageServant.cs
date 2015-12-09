using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models.Language;
using Newtonsoft.Json;

namespace Migree.Core.Servants
{
    public class LanguageServant : ILanguageServant
    {
        public LanguageItem Get<LanguageItem>()
            where LanguageItem : class, ILanguage
        {
            var language = JsonConvert.DeserializeObject<Language>("{\"sendMessageMail\": {    \"subject\": \"You got a Migree-mail from {0}\",    \"fromName\": \"{0} thru Migree\",    \"fromMail\": \"no-reply@migree.se\",    \"message\": \"{0}\n\nReply to this e-mail or send a mail directly to {1}, to get in touch with {2}\"  }}");

            if (typeof(LanguageItem).Name == "SendMessageMail")
            {
                return language.SendMessageMail as LanguageItem;
            }

            return default(LanguageItem);
        }

        public string Get(string languageString, params string[] args)
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
    }
}

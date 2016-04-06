using Migree.Core.Definitions;
using Migree.Core.Interfaces;
using Migree.Core.Interfaces.Models;
using Migree.Core.Models;
using Migree.Core.Models.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migree.Core.Servants
{
    public class BusinessServant : IBusinessServant
    {
        private ILanguageServant LanguageServant { get; }
        public BusinessServant(ILanguageServant languageServant)
        {
            LanguageServant = languageServant;
        }

        public ICollection<IBusiness> GetAll()
        {
            var language = LanguageServant.Get<Definition>().Business;

            return Enum.GetValues(typeof(BusinessGroup))
                .Cast<BusinessGroup>()
                .OrderBy(p => language[p.ToString()])
                .Select(p => new Business
                {
                    Id = (int)p,
                    Type = p,
                    Name = language[p.ToString()]
                }).ToList<IBusiness>();
        }
    }
}

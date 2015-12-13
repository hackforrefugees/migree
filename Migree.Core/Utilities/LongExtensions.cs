using Migree.Core.Models.Language;
using System;

namespace Migree
{
    public static class LongExtensions
    {
        public static string ToRelativeDateTimeString(this long timestamp, RelativeDateTimeStrings languageStrings)
        {
            var timeDifference = DateTime.UtcNow - new DateTime(timestamp);

            if (timeDifference.Days > 1)
            {
                return string.Format(languageStrings.Days, timeDifference.Days);
            }
            else if (timeDifference.Days == 1)
            {
                return languageStrings.Day;
            }
            else if (timeDifference.Hours > 1)
            {
                return string.Format(languageStrings.Hours, timeDifference.Hours);
            }
            else if (timeDifference.Hours == 1)
            {
                return languageStrings.Hour;
            }
            else if (timeDifference.Minutes > 5)
            {
                return string.Format(languageStrings.Minutes, timeDifference.Minutes);
            }
            else
            {
                return languageStrings.Now;
            }
        }
    }
}

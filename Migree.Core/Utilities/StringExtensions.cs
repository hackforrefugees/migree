using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Migree
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string emailToValidate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailToValidate))
                {
                    return false;
                }

                emailToValidate = Regex.Replace(emailToValidate, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                return Regex.IsMatch(emailToValidate,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            var idn = new IdnMapping();
            var domainName = match.Groups[2].Value;
            domainName = idn.GetAscii(domainName);
            return match.Groups[1].Value + domainName;
        }
    }
}

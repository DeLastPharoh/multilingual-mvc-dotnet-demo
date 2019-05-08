using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace MultiLingualApplication
{
    public class LanguageMang
    {
        public static List<LangProperties> AvailableLanguages = new List<LangProperties> {
            new LangProperties {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new LangProperties {
                LanguageFullName = "Arabic", LanguageCultureName = "ar-SA"
            },
            new LangProperties {
                LanguageFullName = "French", LanguageCultureName = "fr-FR"
            },
            new LangProperties {
                LanguageFullName = "Russian", LanguageCultureName = "ru-RU"
            },
        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
    }
    public class LangProperties
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}
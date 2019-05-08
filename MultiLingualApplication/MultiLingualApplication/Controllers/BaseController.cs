using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiLingualApplication.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string language = null;
            HttpCookie languageCookie = Request.Cookies["culture"];
            if (languageCookie != null)
            {
                language = languageCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var _userLanguage = userLanguage != null ? userLanguage[0] : "";
                if (_userLanguage != "")
                {
                    language = _userLanguage;
                }
                else
                {
                    language = LanguageManger.GetDefaultLanguage();
                }
            }
            new LanguageManger().SetLanguage(language);
            return base.BeginExecuteCore(callback, state);
        }
    }
}
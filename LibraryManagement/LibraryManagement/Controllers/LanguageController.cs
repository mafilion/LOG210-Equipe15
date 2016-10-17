using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Utils;

namespace LibraryManagement.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult setLanguage() 
        {
            UtilResources.ChangeLanguageTwo();
            string url = this.Request.UrlReferrer.AbsolutePath;
            return Redirect(url);
        }
    }
}
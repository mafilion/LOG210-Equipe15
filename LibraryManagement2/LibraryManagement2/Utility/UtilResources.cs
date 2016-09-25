using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement2.Models;
using System.Data.Entity;

namespace LibraryManagement2.Utils
{
    public static class UtilResources
    {
        private static int IDLanguage;

        public static void CreateInstance()
        {
            //Aller le chercher en BD et le set
            libraryManagementEntities db = new libraryManagementEntities();
            Settings config = db.Settings.Single();
            IDLanguage = config.IDLanguage;
        }

        public static void ChangeLanguage(string language)
        {
            libraryManagementEntities db = new libraryManagementEntities();
            switch (language)
            {
                case "Fr" :
                    IDLanguage = 1;
                break;
                case "En":
                    IDLanguage = 2;
                break;
                default:
                    IDLanguage = 1;
                break;
            }
            Settings config = db.Settings.Single();
            config.IDLanguage = IDLanguage;
            db.Entry(config).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void ChangeLanguageTwo()
        {
            libraryManagementEntities db = new libraryManagementEntities();
            Settings config = db.Settings.Single();
            if (config.IDLanguage == 1)
            {
                IDLanguage = 2;
            }
            else
            {
                IDLanguage = 1;
            }
            config.IDLanguage = IDLanguage;
            db.Entry(config).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static string GetLabel(string label)
        {
            libraryManagementEntities db = new libraryManagementEntities();
            Resources text = db.Resources.Where(c => c.TextName == label && c.IDLanguage == IDLanguage).First();
            if (text.Description != null && text.Description != "")
                return text.Description;
            else
                return label;
        }
        
    }
}
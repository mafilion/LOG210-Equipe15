using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement.Models;
using System.Data.Entity;

namespace LibraryManagement.Utils
{
    public class UtilResources
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();
        private int IDLanguage;

        UtilResources()
        {
            //Aller le chercher en BD et le set
            Settings config = db.Settings.Single();
            IDLanguage = config.IDLanguage;
        }

        public void ChangeLanguage(string language)
        {
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

  
        public string GetLabel(string label)
        {
            Resources text = db.Resources.Where(c => c.TextName.Contains(label) && c.IDLanguage == IDLanguage).Single();
            if (text.Description != null && text.Description != "")
                return text.Description;
            else
                return label;
        }
        
    }
}
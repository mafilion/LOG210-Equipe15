using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagement.Models;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace LibraryManagement.Utils
{
    public static class UtilResources
    {
 
        private static int IDLanguage;
        public static void CreateInstance()
        {
            //Aller le chercher en BD et le set
            LibraryManagementEntities db = new LibraryManagementEntities();
            Settings config = db.Settings.Single(); //Si plante ici mettre à jour la connexion du ModelDB!
            IDLanguage = config.IDLanguage;
        }

        public static void ChangeLanguage(string language)
        {
            LibraryManagementEntities db = new LibraryManagementEntities();
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
            LibraryManagementEntities db = new LibraryManagementEntities();
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
            LibraryManagementEntities db = new LibraryManagementEntities();
            Resources text = db.Resources.Where(c => c.TextName == label && c.IDLanguage == IDLanguage).FirstOrDefault();
            if (text != null)
                return text.Description;
            else
                return label;
        }

        public static bool SendMail(String courrielDestinataire, String titre, String sujet)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("gestionbiblio10@gmail.com", "#TristesseDunHomme"),
                EnableSsl = true
            };
            client.Send("gestionbiblio10@gmail.com", courrielDestinataire, titre, sujet);

            return true;
        }

        public static string EncryptPassword(string password)
        {
            //Source: http://www.aspnettutorials.com/tutorials/advanced/use-md5-to-encrypt-passwords-with-asp-net-4-0-and-c/
            //create the MD5CryptoServiceProvider object we will use to encrypt the password
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //create an array of bytes we will use to store the encrypted password
            Byte[] hashedBytes;
            //Create a UTF8Encoding object we will use to convert our password string to a byte array
            UTF8Encoding encoder = new UTF8Encoding();

            //encrypt the password and store it in the hashedBytes byte array
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));


            return hashedBytes.ToHex(false);
        }

        //Source function : http://stackoverflow.com/questions/2435695/converting-a-md5-hash-byte-array-to-a-string
        public static string ToHex(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        

    }
}
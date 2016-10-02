using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement2.Models;
using LibraryManagement2.Utils;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace LibraryManagement2.Controllers
{
    public class StudentsController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Students
        public ActionResult Index()
        {
            var student = db.Student.Include(m => m.Email);
            return View(db.Student.ToList());
        }
        
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
        
            return View();
        }

        // POST: Students/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDStudent,FirstName,LastName,Email,PhoneNumber,StudentPassword")] Student student)
        {
     
            if (ModelState.IsValid && validForm())
            {
                student.StudentPassword = UtilResources.EncryptPassword(Request.Form["password1"]);

            
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", UtilResources.GetLabel("Tous les champs doivent contenir une valeur"));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDStudent,FirstName,LastName,Email,PhoneNumber,StudentPassword")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Valide si les informations entrés par l'utilisateur sont corrects
        public bool validForm()
        {
            bool valid = true;

            //validation si les mots de passes sont pareils
            if (Request.Form["password1"] == null || Request.Form["password2"] == null || (Request.Form["password2"] != Request.Form["password1"]))
            {
                valid = false;
                ModelState.AddModelError("", UtilResources.GetLabel("Les mots de passe ne correspondent pas"));
            }


            // Numero Telephone validation
            // L'utilisateur à le choix de ne pas écrire son # de téléphone 
            if (Request.Form["PhoneNumber"] != "")
            {
                // À l'aide des Regular Expression, on regarde si le numéro de téléphone est du bon format. 
                string strIn = Request.Form["PhoneNumber"];
                string re1 = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";  // RegEx du numero de telephone

                Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = r.Match(strIn);
                if (m.Success)
                {
                    String int1 = m.Groups[1].ToString();
                    Console.Write("(" + int1.ToString() + ")" + "\n");

                    // On doit également vérifier si le numéro n'est pas déjà dans la base de donnée
                    if (db.Student.Any(o => o.PhoneNumber == strIn))
                    {
                        valid = false;
                        ModelState.AddModelError("", UtilResources.GetLabel("Ce numéro de téléphone est déjà utilisé"));
                    }
                }
                else
                {
                    valid = false;
                    ModelState.AddModelError("", UtilResources.GetLabel("Numéro de téléphone doit être sous le format: xxx-xxx-xxxx"));
                }
            }

            // Email validation
            string strEmail = Request.Form["Email"];

            if (strEmail != "" || strEmail != null)
            {
                try
                {
                    MailAddress mail = new MailAddress(strEmail);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("",UtilResources.GetLabel("L'adresse courriel n'est pas valide."));
                    valid = false;
                }
            }
            else
            {
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel n'est pas valide."));
                valid = false;
            }

            // On regarde si l'email est déjà en BD
            if (db.Student.Any(o => o.Email == strEmail))
            {
                valid = false;
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel est déjà utilisé"));
            }
            return valid;
        }
    }
}

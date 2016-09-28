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
            //Ajouter les erreurs ici 
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
            }


            // Numero Telephone validation
            /*
            try
            {
                string strIn = Request.Form["PhoneNumber"];
                Regex.IsMatch(strIn,
                    @"^[1 - 9]\d$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            } catch (RegexMatchTimeoutException)
            {
                valid = false;
                
            }
            */

            // Numero Telephone validation
            string strIn = Request.Form["PhoneNumber"];
            string re1 = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";  // Integer Number

            Regex r = new Regex(re1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(strIn);
            if (m.Success)
            {
                String int1 = m.Groups[1].ToString();
                Console.Write("(" + int1.ToString() + ")" + "\n");
            }else
            {
                valid = false;
            }




            return valid;





        }
    }
}

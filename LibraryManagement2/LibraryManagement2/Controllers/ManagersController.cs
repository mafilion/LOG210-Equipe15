using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement2.Models;
using System.Net.Mail;
using LibraryManagement2.Utils;

namespace LibraryManagement2.Controllers
{
    public class ManagersController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Managers
        public ActionResult Index()
        {
            var manager = db.Manager.Include(m => m.Cooperative);
            return View(manager.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            ViewBag.IDCooperative = new SelectList(db.Cooperative, "IDCooperative", "Name");
            return View();
        }

        // POST: Managers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagersCooperativesViewModels managerCoop)
        {
            if (ModelState.IsValid && validForm(managerCoop))
            {
                db.Cooperative.Add(managerCoop.cooperative);
                db.SaveChanges();

                managerCoop.manager.IDCooperative = managerCoop.cooperative.IDCooperative;
                managerCoop.manager.ManagerPassword = UtilResources.EncryptPassword(Request.Form["password1"]);

                db.Manager.Add(managerCoop.manager);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(managerCoop);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCooperative = new SelectList(db.Cooperative, "IDCooperative", "Name", manager.IDCooperative);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDManager,IDCooperative,FirstName,LastName,Email,ManagerPassword")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCooperative = new SelectList(db.Cooperative, "IDCooperative", "Name", manager.IDCooperative);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Manager.Find(id);
            db.Manager.Remove(manager);
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

        public bool validForm(ManagersCooperativesViewModels managerCoop)
        {
            bool valid = true;

            //validation si les mots de passes sont pareils
            if (Request.Form["password1"] == "" || Request.Form["password2"] == "" || (Request.Form["password2"] != Request.Form["password1"]))
            {
                valid = false;
            }

            //validation si la cooprative existe déjà dans la base de données
            if(db.Cooperative.Any(o => o.Name == managerCoop.cooperative.Name))
            {
                valid = false;
            }

            //validation pour le courriel
            try
            {
                MailAddress m = new MailAddress(managerCoop.manager.Email);
            }
            catch (FormatException)
            {
                valid = false;
            }

            return valid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
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
                // Si la coopérative existe, on ne l'ajoute pas en BD
                if (managerCoop.manager.IDCooperative == 0 || managerCoop.manager.IDCooperative == null)
                {
                    db.Cooperative.Add(managerCoop.cooperative);
                    db.SaveChanges();
                    managerCoop.manager.IDCooperative = managerCoop.cooperative.IDCooperative;
                }
                
                managerCoop.manager.ManagerPassword = UtilResources.EncryptPassword(Request.Form["password1"]);

                db.Manager.Add(managerCoop.manager);
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", UtilResources.GetLabel("Tous les champs doivent contenir une valeur"));
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
                ModelState.AddModelError("", UtilResources.GetLabel("Les mots de passe ne correspondent pas"));
            }

            //validation si la cooperative existe déjà dans la base de données
            if(db.Cooperative.Any(o => o.Name == managerCoop.cooperative.Name))
            {
                //Met à jour les données de la coopérative
                Cooperative temp = db.Cooperative.Where(o => o.Name == managerCoop.cooperative.Name).First();
                managerCoop.manager.IDCooperative = temp.IDCooperative;
                temp.NoStreet = managerCoop.cooperative.NoStreet;
                temp.Street = managerCoop.cooperative.Street;
                temp.PostalCode = managerCoop.cooperative.PostalCode;
                temp.City = managerCoop.cooperative.City;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }

            //validation pour le courriel
            if (managerCoop.manager.Email != "" || managerCoop.manager.Email != null)
            {
                try
                {
                    MailAddress mail = new MailAddress(managerCoop.manager.Email);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel n'est pas valide."));
                    valid = false;
                }
            }
            else
            {
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel n'est pas valide."));
                valid = false;
            }

            // On regarde si l'email est déjà en BD
            if (db.Manager.Any(o => o.Email == managerCoop.manager.Email))
            {
                valid = false;
                ModelState.AddModelError("", UtilResources.GetLabel("L'adresse courriel est déjà utilisé"));
            }

            return valid;
        }
    }
}

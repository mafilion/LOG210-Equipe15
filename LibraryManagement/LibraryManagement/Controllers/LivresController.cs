using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement2.Models;

namespace LibraryManagement2.Controllers
{
    public class LivresController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Livres
        public ActionResult Index()
        {
            var livre = db.Livre.Include(l => l.EtatLivre);
            return View(livre.ToList());
        }

        // GET: Livres/Create
        public ActionResult Create()
        {
            ViewBag.IDEtatLivre = new SelectList(db.EtatLivre, "IDEtatLivre", "Description");
            return View();
        }

        // POST: Livres/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLivre,noISBN,noEAN,noUPC,Titre,nbPages,prix,IDEtatLivre")] LivresAuteursViewModel livres)
        {
            if (ModelState.IsValid)
            {
                //db.Livre.Add(livre);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEtatLivre = new SelectList(db.EtatLivre, "IDEtatLivre", "Description", livres.livre.IDEtatLivre);
            return View(livres);
        }

        [HttpPost]
        [ActionName("SearchBooks")]
        public ActionResult SearchBooks(string Number)
        {
            //Recherche dans la BD si le livre existe (avec le Number sur le ISBN/EAN/UPC
            //Recherche dans l'api
            //Récupérer les informations et les retourner en json
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

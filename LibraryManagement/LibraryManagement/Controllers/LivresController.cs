using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Google.Apis.Books.v1.Data;

namespace LibraryManagement.Controllers
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
        public ActionResult CreateBook( Livre livres)
        {
            Console.WriteLine(livres.Titre);

            return View(livres);
        }

        [HttpPost]
        [ActionName("SearchBooks")]
        public async System.Threading.Tasks.Task<ActionResult> SearchBooks(string Number)
        {
            System.Diagnostics.Debug.WriteLine("Je suis dans la méthode!!!");
            System.Diagnostics.Debug.WriteLine("Nombre: " + Number);
            Livre livre = new Livre();
            Volume volume = new Volume();

            //Recherche dans la BD si le livre existe (avec le Number sur le ISBN/EAN/UPC
            if (db.Livre.Any(o => o.noISBN == Number || o.noEAN == Number || o.noUPC == Number))
            {
                livre = db.Livre.Where(o => o.noISBN == Number || o.noEAN == Number || o.noUPC == Number).First();
            }
            else
            {
                //Recherche dans l'api
                //SOURCE installation API https://xinyustudio.wordpress.com/2014/12/18/google-book-search-in-c-a-step-by-step-walk-through-tutorial/
                //À tester
                var result = await service.Volumes.List(Number).ExecuteAsync();
                if (result != null)
                {
                    volume = result.Items.FirstOrDefault();
                }

                if (volume != null)
                {
                    livre.Titre = volume.VolumeInfo.Title;
                    livre.nbPages = (int)volume.VolumeInfo.PageCount;
                    livre.prix = (double)volume.SaleInfo.RetailPrice.Amount;
                    livre.noISBN = volume.VolumeInfo.IndustryIdentifiers[0].Identifier;
                    //auteur

                }

            }
            //Récupérer les informations et les retourner en json
            return Json(livre, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       public static BooksService service = new BooksService(
       new BaseClientService.Initializer
       {
           ApplicationName = "librarymanagement2",
           ApiKey = "AIzaSyAhY2jOLfkBn5i3lSUISaTkbgWTPex8xzA",
       });
    }
}

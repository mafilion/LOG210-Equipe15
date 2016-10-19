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
        public ActionResult CreateBook(LivresAuteursViewModel livresAut)
        {
            Auteur autExist = db.Auteur.Where(c => c.Name == livresAut.Aut.Name).FirstOrDefault();
            Auteur aut = new Auteur();
            Livre li = new Livre();
            LivresAuteurs liA = new LivresAuteurs();
            if (autExist == null)
            {
                //Pas présent en BD, on l'ajoute
                aut.Name = livresAut.Aut.Name;
                db.Auteur.Add(aut);
                db.SaveChanges();
            }
            else
            {
                aut = autExist;
            }
            //Ajouter le livre + liaison état
            li.IDEtatLivre = livresAut.livre.IDEtatLivre;
            li.Titre = livresAut.livre.Titre;
            li.nbPages = livresAut.livre.nbPages;
            li.prix = livresAut.livre.prix;
            li.noISBN = livresAut.livre.noISBN;
            db.Livre.Add(li);
            db.SaveChanges();
            //on ajoute juste un Auteur pour l'instant ("YOLO")
            liA.IDLivre = li.IDLivre;
            liA.IDAuteur = aut.IDAuteur;
            db.LivresAuteurs.Add(liA);
            db.SaveChanges();

            //Récupérer les informations et les retourner en json
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SearchBooks")]
        public async System.Threading.Tasks.Task<ActionResult> SearchBooks(string Number)
        {
            LivresAuteursViewModel LivreAut = new LivresAuteursViewModel();
            Livre livre = new Livre();
            Auteur Aut = new Auteur();
            Volume volume = new Volume();
            LivreAut.ListAuteur = new List<Auteur>();

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
                    try
                    {
                        livre.Titre = volume.VolumeInfo.Title;
                    }
                    catch
                    {
                        livre.Titre = "";
                    }
                    try
                    {
                        livre.nbPages = (int)volume.VolumeInfo.PageCount;
                    }
                    catch
                    {
                        livre.nbPages = 0;
                    }
                    try
                    {
                        livre.prix = (double)volume.SaleInfo.RetailPrice.Amount;
                    }
                    catch
                    {
                        livre.prix = 0.00;
                    }
                    livre.noISBN = volume.VolumeInfo.IndustryIdentifiers[0].Identifier;
                

                    /*for(int i=0;i < volume.VolumeInfo.Authors.Count && i<5;i++)
                    {
                        Aut.Name = volume.VolumeInfo.Authors[i];
                        LivreAut.ListAuteur.Add(Aut);
                        Aut = new Auteur();
                    }*/
                    try
                    {
                        Aut.Name = volume.VolumeInfo.Authors[0];
                    }
                    catch
                    {
                        Aut.Name = "";
                    }
                    LivreAut.livre = livre;
                    LivreAut.Aut = Aut;
                }

            }
            //Récupérer les informations et les retourner en json
            return Json(LivreAut, JsonRequestBehavior.AllowGet);
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
           ApplicationName = "librarymanagement",
           ApiKey = "AIzaSyAhY2jOLfkBn5i3lSUISaTkbgWTPex8xzA",
       });
    }
}

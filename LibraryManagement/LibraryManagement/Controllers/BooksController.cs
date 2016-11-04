using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Google.Apis.Books.v1.Data;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: Livres/Create
        public ActionResult Create()
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == false )
            {
                ViewBag.IDBookState = new SelectList(db.BookState, "IDBookState", "Description");
                return View();
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginStudents", "Accounts");
        }

        // POST: Livres/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLivre,noISBN,noEAN,noUPC,Titre,nbPages,prix,IDEtatLivre")] BooksAuthorsViewModel books)
        {
            if (ModelState.IsValid)
            {
                //db.Livre.Add(livre);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDEtatLivre = new SelectList(db.BookState, "IDBookState", "Description", books.bookState.IDBookState);
            return View(books);
        }

        [HttpPost]
        public ActionResult CreateBook(BooksAuthorsViewModel booksAut)
        {
            Author autExist = db.Author.Where(c => c.Name == booksAut.Aut.Name).FirstOrDefault();
            Author aut = new Author();
            Book bo = db.Book.Where(c => c.noISBN == booksAut.book.noISBN).FirstOrDefault();
            BooksAuthors boA = new BooksAuthors();
            BooksCopy boC = new BooksCopy();
            if (autExist == null)
            {
                //Pas présent en BD, on l'ajoute
                aut.Name = booksAut.Aut.Name;
                db.Author.Add(aut);
                db.SaveChanges();
            }
            else
            {
                aut = autExist;
            }

            if (bo == null)
            {
                //Ajouter le livre + liaison état
                bo = new Book();
                bo.Title = booksAut.book.Title;
                bo.nbPages = booksAut.book.nbPages;
                bo.price = booksAut.book.price;
                bo.noISBN = booksAut.book.noISBN;
                db.Book.Add(bo);
                db.SaveChanges();
                //on ajoute juste un Auteur pour l'instant ("YOLO")
                boA.IDBook = bo.IDBook;
                boA.IDAuthor = aut.IDAuthor;
                db.BooksAuthors.Add(boA);
                db.SaveChanges();
            }
            else
            {
                //Met à jour le livre
                bo.Title = booksAut.book.Title;
                bo.nbPages = booksAut.book.nbPages;
                bo.price = booksAut.book.price;
                bo.noISBN = booksAut.book.noISBN;
                db.Entry(bo).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Ajouter l'exemplaire
            boC.IDBook = bo.IDBook;
            boC.IDBookState = booksAut.bookState.IDBookState;
            boC.IDStudent = AccountManagement.getIDAccount();
            boC.Available = -1;
            db.BooksCopy.Add(boC);
            db.SaveChanges();

            //Récupérer les informations et les retourner en json
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SearchBooks")]
        public async System.Threading.Tasks.Task<ActionResult> SearchBooks(string Number)
        {
            BooksAuthorsViewModel BookAut = new BooksAuthorsViewModel();
            Book book = new Book();
            Author Aut = new Author();
            Volume volume = new Volume();
            BookAut.AuthorList = new List<Author>();

            //Recherche dans la BD si le livre existe (avec le Number sur le ISBN/EAN/UPC
            if (db.Book.Any(o => o.noISBN == Number || o.noEAN == Number || o.noUPC == Number))
            {
                BookAut.book = db.Book.Where(o => o.noISBN == Number || o.noEAN == Number || o.noUPC == Number).First();
                BooksAuthors bookAuts = db.BooksAuthors.Where(o => o.IDBook == BookAut.book.IDBook).First();
                BookAut.Aut = db.Author.Where(o => o.IDAuthor == bookAuts.IDAuthor).First();
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
                        book.Title = volume.VolumeInfo.Title;
                    }
                    catch
                    {
                        book.Title = "";
                    }
                    try
                    {
                        book.nbPages = (int)volume.VolumeInfo.PageCount;
                    }
                    catch
                    {
                        book.nbPages = 0;
                    }
                    try
                    {
                        book.price = (double)volume.SaleInfo.RetailPrice.Amount;
                    }
                    catch
                    {
                        book.price = 0.00;
                    }
                    book.noISBN = volume.VolumeInfo.IndustryIdentifiers[0].Identifier;
                

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
                    BookAut.book = book;
                    BookAut.Aut = Aut;
                }

            }
            //Récupérer les informations et les retourner en json
            return Json(BookAut, JsonRequestBehavior.AllowGet);
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

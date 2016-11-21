using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Utils;

namespace LibraryManagement.Controllers
{
    public class BookDeliveryController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: BookDelivery
        public ActionResult Index()
        {
            return View(db.Book.ToList());
        }

        // GET: BookDelivery/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: BookDelivery/Create
        public ActionResult Create()
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == true)
            {
                ViewBag.IDBookState = new SelectList(db.BookState, "IDBookState", "Description");
                ViewBag.IDCooperative = new SelectList(db.Cooperative, "IDCooperative", "Name");
                return View();
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginManagers", "Accounts");
          
        }

        // POST: BookDelivery/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBook,noISBN,noEAN,noUPC,Title,nbPages,price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [HttpPost]
        [ActionName("SearchBooks")]
        public ActionResult SearchBooks(string Value)
        {
            var Books = (from B in db.Book
                                join BA in db.BooksAuthors on B.IDBook equals BA.IDBook
                                join A in db.Author on BA.IDAuthor equals A.IDAuthor
                                join BC in db.BooksCopy on B.IDBook equals BC.IDBook
                                join C in db.Cooperative on BC.IDCooperative equals C.IDCooperative
                                join S in db.Student on BC.IDStudent equals S.IDStudent
                                join BS in db.BookState on BC.IDBookState equals BS.IDBookState
                                where (B.noISBN == Value || B.noUPC == Value || B.noEAN == Value || B.Title.Contains(Value) || S.FirstName + " " + S.LastName == Value || A.Name.Contains(Value)) && BC.Available == -1
                                select new {BC.IDBooksCopy,B.noISBN, B.Title, AuthorName = A.Name, S.FirstName, S.LastName, BS.Description,B.price, BS.PricePercentage,C.IDCooperative, CoopName = C.Name }).ToList();

            //Récupérer les informations et les retourner en json
            return Json(Books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("getBookCopy")]
        public ActionResult getBookCopy(string id)
        {
            int idBookCopy = Int32.Parse(id);
            var Books = (from BC in db.BooksCopy
                         join C in db.Cooperative on BC.IDCooperative equals C.IDCooperative
                         join BA in db.BooksAuthors on BC.Book.IDBook equals BA.IDBook
                         join A in db.Author on BA.IDAuthor equals A.IDAuthor
                         where BC.IDBooksCopy == idBookCopy
            select new { BC.IDBooksCopy,BC.Book.noISBN, BC.Book.Title, BC.Student.FirstName, BC.Student.LastName, BC.BookState.IDBookState, BC.BookState.PricePercentage, BC.Book.price, AuthorName = A.Name, BC.IDStudent,C.IDCooperative , CoopName = C.Name }).SingleOrDefault();

            //Récupérer les informations et les retourner en json
            return Json(Books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("depositBook")]
        public ActionResult depositBook(string IDBooksCopy, string IDBookState)
        {
            int idBookCopy = Int32.Parse(IDBooksCopy);
            int idBookState = Int32.Parse(IDBookState);

            //updater truc dans bd
            BooksCopy book = db.BooksCopy.Where(c => c.IDBooksCopy == idBookCopy).FirstOrDefault();
            book.Available = 1;
            book.IDBookState = idBookState;
            db.SaveChanges();

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SendMail")]
        public ActionResult SendMail(string IDStudent)
        {
            int idStudent = Int32.Parse(IDStudent);

            //updater truc dans bd
            Student s = db.Student.Where(c => c.IDStudent == idStudent).FirstOrDefault();
            Utils.UtilResources.SendMail(s.Email, UtilResources.GetLabel("TitreMailDepot"), UtilResources.GetLabel("SujetMailDepot"));

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        // GET: BookDelivery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BookDelivery/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBook,noISBN,noEAN,noUPC,Title,nbPages,price")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: BookDelivery/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: BookDelivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
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
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;

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
            ViewBag.IDBookState = new SelectList(db.BookState, "IDBookState", "Description");
            return View();
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
                                join S in db.Student on BC.IDStudent equals S.IDStudent
                                join BS in db.BookState on BC.IDBookState equals BS.IDBookState
                                where (B.noISBN == Value || B.noUPC == Value || B.noEAN == Value || B.Title == Value || S.FirstName + " " + S.LastName == Value) && BC.Available == -1
                                select new {BC.IDBooksCopy,B.noISBN, B.Title, A.Name, S.FirstName, S.LastName, BS.Description,B.price, BS.PricePercentage }).ToList();

            //Récupérer les informations et les retourner en json
            return Json(Books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("getBookCopy")]
        public ActionResult getBookCopy(string id)
        {
            int idBookCopy = Int32.Parse(id);
            var Books = (from BC in db.BooksCopy
                         join BA in db.BooksAuthors on BC.Book.IDBook equals BA.IDBook
                         join A in db.Author on BA.IDAuthor equals A.IDAuthor
                         where BC.IDBooksCopy == idBookCopy
            select new { BC.IDBooksCopy,BC.Book.noISBN, BC.Book.Title, BC.Student.FirstName, BC.Student.LastName, BC.BookState.IDBookState, BC.BookState.PricePercentage, BC.Book.price, A.Name }).SingleOrDefault();

            //Récupérer les informations et les retourner en json
            return Json(Books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("depositBook")]
        public void depositBook(string IDBooksCopy, string IDBookState)
        {
            int idBookCopy = Int32.Parse(IDBooksCopy);
            int idBookState = Int32.Parse(IDBookState);
        
            //updater truc dans bd
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

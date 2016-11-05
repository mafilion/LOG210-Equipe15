﻿using System;
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
    public class DeliveriesController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: Deliveries
        public ActionResult Index()
        {
            if(AccountManagement.isConnected() != null && AccountManagement.getEstManager() == true)
            {
                List<BookingLine> BookingLineList = db.BookingLine.Where(b => b.BookingState == -1).ToList();
                List<Booking> BookingList = new List<Booking>();
                foreach (BookingLine element in BookingLineList)
                {
                    if(BookingList.Where(b => b.IDBooking == element.IDBooking).SingleOrDefault() == null)
                    {
                        BookingList.Add(db.Booking.Where(b => b.IDBooking == element.IDBooking).Include(b => b.Student).Single());
                    }
                }
                return View(BookingList);
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginManagers", "Accounts");
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == true)
            {
                DeliveriesViewModel DelModel = new DeliveriesViewModel();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Booking booking = db.Booking.Find(id);
                if (booking == null)
                {
                    return HttpNotFound();
                }
                List<SelectListItem> selectList = new List<SelectListItem>()
                {
                    new SelectListItem() {Text=UtilResources.GetLabel("Attente de l'étudiant"), Value="-1"},
                    new SelectListItem() {Text=UtilResources.GetLabel("Refusé"), Value="0" },
                    new SelectListItem() {Text=UtilResources.GetLabel("Accepté"), Value="1" }
                };
                DelModel.bookingLineList = new List<BookingLine>();
                DelModel.booksCopyList = new List<BooksCopy>();
                DelModel.booksStateList = new List<BookState>();
                DelModel.bookList = new List<Book>();
                DelModel.authorList = new List<Author>();
                DelModel.booking = booking;
                DelModel.student = db.Student.Where(stu => stu.IDStudent == booking.IDStudent).FirstOrDefault();

                var Query = from bl in db.BookingLine
                            join bc in db.BooksCopy on bl.IDBooksCopy equals bc.IDBooksCopy
                            join bs in db.BookState on bc.IDBookState equals bs.IDBookState
                            join b in db.Book on bc.IDBook equals b.IDBook
                            join ba in db.BooksAuthors on b.IDBook equals ba.IDBook
                            join a in db.Author on ba.IDAuthor equals a.IDAuthor
                            where bl.IDBooking == DelModel.booking.IDBooking && bl.BookingState == -1
                            select new { bl, bc, bs, b, a };

                foreach (var lines in Query)
                {
                    ViewData["StateBookingLine" + lines.bl.IDBookingLine] = selectList;
                    DelModel.bookingLineList.Add(lines.bl);
                    DelModel.booksCopyList.Add(lines.bc);
                    DelModel.booksStateList.Add(lines.bs);
                    lines.b.price = (lines.b.price * lines.bs.PricePercentage);
                    DelModel.bookList.Add(lines.b);
                    DelModel.authorList.Add(lines.a);
                }
                return View(DelModel);
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginManagers", "Accounts");
        }

        // POST: Bookings/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeliveriesViewModel bookingView)
        {
            bool tradeFullyCompleted = true;
            int IDTemp =0;
            BooksCopy book = new BooksCopy();
            BookingLine bl = new BookingLine();
            if (ModelState.IsValid)
            {
                //Étapes
                for(int i =0; i< bookingView.bookingLineList.Count();i++)
                {
                    IDTemp = bookingView.bookingLineList[i].IDBookingLine;
                    switch (Request.Form["StateBookingLine" + bookingView.bookingLineList[i].IDBookingLine])
                    {
                        case "0":
                            bl = db.BookingLine.Single(bls => bls.IDBookingLine == IDTemp);
                            bl.BookingState = 0;
                            IDTemp = bookingView.bookingLineList[i].IDBooksCopy;
                            book = db.BooksCopy.Single(bo => bo.IDBooksCopy == IDTemp);
                            book.Available = 1; //L'exemplaire est de nouveau disponible pour les autres étudiants.
                            db.Entry(bl).State = EntityState.Modified;
                            db.Entry(book).State = EntityState.Modified;
                            db.SaveChanges();
                            //Envoyé un courriel de confirmation à l'étudiant.
                            break;
                        case "1":
                                bl = db.BookingLine.Single(bls => bls.IDBookingLine == IDTemp);
                                bl.BookingState = 1;
                                IDTemp = bookingView.bookingLineList[i].IDBooksCopy;
                                book = db.BooksCopy.Single(bo => bo.IDBooksCopy == IDTemp);
                                book.Available = 0;
                                db.Entry(bl).State = EntityState.Modified;
                                db.Entry(book).State = EntityState.Modified;
                                db.SaveChanges();
                            break;
                        default: //-1
                            tradeFullyCompleted = false;
                            break;
                    }
                }
                if (tradeFullyCompleted == true)
                {
                    bookingView.booking.IDManager = AccountManagement.getIDAccount();
                    bookingView.booking.TradeConfirmation = true;
                    db.Entry(bookingView.booking).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(bookingView);
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

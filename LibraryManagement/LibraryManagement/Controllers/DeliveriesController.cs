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
                DelModel.booking = booking;
                DelModel.student = db.Student.Where(stu => stu.IDStudent == booking.IDStudent).FirstOrDefault();
                DelModel.bookingLineList = db.BookingLine.Where(bl => bl.IDBooking == booking.IDBooking && bl.BookingState==-1).ToList();

                List<SelectListItem> selectList = new List<SelectListItem>()
                {
                    new SelectListItem() {Text=UtilResources.GetLabel("Attente de l'étudiant"), Value="-1"},
                    new SelectListItem() {Text=UtilResources.GetLabel("Refusé"), Value="0" },
                    new SelectListItem() {Text=UtilResources.GetLabel("Accepté"), Value="1" }
                };
                ViewBag.StateBookingLine = selectList;

                //Test Temp
                //On ajoute les exemplaires
                DelModel.booksCopyList = new List<BooksCopy>();
                foreach (var line in DelModel.bookingLineList)
                {
                    DelModel.booksCopyList.Add(db.BooksCopy.Single(bo => bo.IDBooksCopy == line.IDBooksCopy));
                }
                //On ajoute l'état des exemplaires
                DelModel.booksStateList = new List<BookState>();
                foreach (var lines in DelModel.booksCopyList)
                {
                    DelModel.booksStateList.Add(db.BookState.Single(bo => bo.IDBookState == lines.IDBookState));
                }
                //On ajoute les détails des livres
                DelModel.bookList = new List<Book>();
                foreach (var linesBook in DelModel.booksCopyList)
                {
                    DelModel.bookList.Add(db.Book.Single(bo => bo.IDBook == linesBook.IDBook));
                }
                //AuteurBooks(Pas nécéssaire dans le viewModel
                List<BooksAuthors> BatempList = new List<BooksAuthors>();
                foreach (var linesBooks in DelModel.bookList)
                {
                    BatempList.Add(db.BooksAuthors.Single(bo => bo.IDBook == linesBooks.IDBook));
                }
                //Auteur 
                DelModel.authorList = new List<Author>();
                foreach (var linesAuthor in BatempList)
                {
                    DelModel.authorList.Add(db.Author.Single(bo => bo.IDAuthor == linesAuthor.IDAuthor));
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
            BooksCopy book = new BooksCopy();
            BookingLine bl = new BookingLine();
            if (ModelState.IsValid)
            {
                //Étapes
                //1.Boucle sur les bookingLine.count()
                for(int i =0; i< bookingView.bookingLineList.Count();i++)
                { 
                switch (Request.Form["Book#"+i])
                    {
                        case "0":
                            bl = db.BookingLine.Single(bls => bls.IDBookingLine == bookingView.bookingLineList[i].IDBookingLine);
                            bl.BookingState = 0;
                            book = db.BooksCopy.Single(bo => bo.IDBooksCopy == bookingView.bookingLineList[i].IDBooksCopy);
                            book.Available = 1; //L'exemplaire est de nouveau disponible pour les autres étudiants.
                            db.Entry(bl).State = EntityState.Modified;
                            db.Entry(book).State = EntityState.Modified;
                            db.SaveChanges();
                            //Envoyé un courriel de confirmation à l'étudiant.
                            break;
                        case "1":
                                bl = db.BookingLine.Single(bls => bls.IDBookingLine == bookingView.bookingLineList[i].IDBookingLine);
                                bl.BookingState = 1;
                                db.Entry(bl).State = EntityState.Modified;
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

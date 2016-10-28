using System;
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
    public class DeliveriesController : Controller
    {
        private libraryManagementEntities db = new libraryManagementEntities();

        // GET: Deliveries
        public ActionResult Index()
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

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDManager = new SelectList(db.Manager, "IDManager", "FirstName", booking.IDManager);
            ViewBag.IDStudent = new SelectList(db.Student, "IDStudent", "FirstName", booking.IDStudent);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBooking,IDStudent,IDManager,BookingDate,TradeConfirmation")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDManager = new SelectList(db.Manager, "IDManager", "FirstName", booking.IDManager);
            ViewBag.IDStudent = new SelectList(db.Student, "IDStudent", "FirstName", booking.IDStudent);
            return View(booking);
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

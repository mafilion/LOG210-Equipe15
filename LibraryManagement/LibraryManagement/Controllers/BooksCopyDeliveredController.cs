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
    public class BooksCopyDeliveredController : Controller
    {
        private LibraryManagementEntities db = new LibraryManagementEntities();

        // GET: BooksCopyTransfer
        public ActionResult Index()
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == true)
            {
                int IDManager = AccountManagement.getIDAccount();
                Manager man = db.Manager.Where(mans => mans.IDManager == IDManager).Single();
                List<BooksCopyTransferLine> booksCopyTransferLine = db.BooksCopyTransferLine.Include(b => b.BooksCopy).Include(b => b.BooksCopyTransfer).Where(b => b.State == 0 && b.IDCooperativeTo == man.IDCooperative).ToList();
                foreach (BooksCopyTransferLine item in booksCopyTransferLine)
                {
                    item.BooksCopy.Book.price = (item.BooksCopy.Book.price * item.BooksCopy.BookState.PricePercentage);
                }
                return View(booksCopyTransferLine.ToList());
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginManagers", "Accounts");
        }

        // GET: BooksCopyTransfer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == true)
            {
                BooksCopyTransferViewModel bctv = new BooksCopyTransferViewModel();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                bctv.bctl = db.BooksCopyTransferLine.Find(id);
                if (bctv.bctl == null)
                {
                    return HttpNotFound();
                }

                var Query = from transferLine in db.BooksCopyTransferLine
                            join transfer in db.BooksCopyTransfer on transferLine.IDBooksCopyTransfer equals transfer.IDBooksCopyTransfer
                            join bc in db.BooksCopy on transferLine.IDBooksCopy equals bc.IDBooksCopy
                            join b in db.Book on bc.IDBook equals b.IDBook
                            join ba in db.BooksAuthors on b.IDBook equals ba.IDBook
                            join a in db.Author on ba.IDAuthor equals a.IDAuthor
                            join coopFrom in db.Cooperative on transferLine.IDCooperativeFrom equals coopFrom.IDCooperative
                            join coopTo in db.Cooperative on transferLine.IDCooperativeTo equals coopTo.IDCooperative
                            where transferLine.IDBooksCopyTransferLine == id
                            select new { transferLine, transfer, bc, coopFrom, coopTo, a };
                bctv.aut = Query.First().a;
                bctv.bct = Query.First().transfer;
                bctv.booksCopy = Query.First().bc;
                bctv.booksCopy.Book.price = (bctv.booksCopy.Book.price * bctv.booksCopy.BookState.PricePercentage);
                bctv.coopFrom = Query.First().coopFrom;
                bctv.coopTo = Query.First().coopTo;
                return View(bctv);
            }
            return RedirectToAction("LoginManagers", "Accounts");
        }

        // POST: BooksCopyTransfer/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BooksCopyTransferViewModel bctv)
        {
            if (ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;
                int id = Int32.Parse(Request.Form["bctl.IDBooksCopyTransferLine"]);

                var Query = from transferLine in db.BooksCopyTransferLine
                            join transfer in db.BooksCopyTransfer on transferLine.IDBooksCopyTransfer equals transfer.IDBooksCopyTransfer
                            join bc in db.BooksCopy on transferLine.IDBooksCopy equals bc.IDBooksCopy
                            join bl in db.BookingLine on bc.IDBooksCopy equals bl.IDBooksCopy
                            join bo in db.Booking on bl.IDBooking equals bo.IDBooking
                            join stu in db.Student on bo.IDStudent equals stu.IDStudent
                            where transferLine.IDBooksCopyTransferLine == id && bl.BookingState == -2 && bo.IDCooperative == transferLine.IDCooperativeTo && bc.Available == 0
                            select new { transferLine, transfer, bc, bl, bo, stu };
                bctv.bctl = Query.First().transferLine;
                bctv.bct = Query.First().transfer;
                bctv.booksCopy = Query.First().bc;
                Booking book = Query.First().bo;
                BookingLine bol = Query.First().bl;
                Student student = Query.First().stu;

                bctv.bctl.State = 1;
                bctv.bct.TransferConfirmation = true;
                bol.BookingState = -1;
                bctv.booksCopy.IDCooperative = bctv.bctl.IDCooperativeTo;

                db.Entry(bctv.bctl).State = EntityState.Modified;
                db.Entry(bctv.bct).State = EntityState.Modified;
                db.Entry(bol).State = EntityState.Modified;
                db.Entry(bctv.booksCopy).State = EntityState.Modified;

                db.SaveChanges();
                sendEmail(bctv.booksCopy,book,student,bctv.bctl);
                return RedirectToAction("Index");
            }
            return View(bctv);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void sendEmail(BooksCopy bc, Booking bo, Student stu,BooksCopyTransferLine bctl)
        {
            string EmailContent = UtilResources.GetLabel("Information Courriel ReceptionLivre1") +bctl.Cooperative.Name;
            EmailContent = EmailContent + "\n" + "------------------------------------------------------------------------------------------------------";
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("Titre") + ": " + bc.Book.Title;
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("Numero ISBN/EAN/UPC") + " :" + bc.Book.noISBN;
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("Nombre de pages") + " :" + bc.Book.nbPages;
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("État du livre") + " :" + bc.BookState.Description;
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("Prix") + " :" + (bc.Book.price * bc.BookState.PricePercentage) + "$";
            EmailContent = EmailContent + " \n";
            EmailContent = EmailContent + "------------------------------------------------------------------------------------------------------";
            EmailContent = EmailContent + UtilResources.GetLabel("Information Courriel ReceptionLivre2");

            UtilResources.SendMail(stu.Email, UtilResources.GetLabel("Confirmation réception de livre pour la réservation") + " #" + bo.IDBooking, UtilResources.GetLabel("Bonjour") + " " + stu.FirstName + " " + stu.LastName + ", \n\n" + EmailContent);
        }
    }
}

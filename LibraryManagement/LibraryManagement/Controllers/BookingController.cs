using LibraryManagement.Models;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookingController : Controller
    {

        private LibraryManagementEntities db = new LibraryManagementEntities();

        private int IDLivreTrouver = 0;

        // Retourne les réservations du student
        public ActionResult Index([DefaultValue(0)]int idBooking)
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == false)
            {
                if (idBooking != 0)
                {

                    List<Booking> BookingList = db.Booking.Where(bo => bo.IDBooking == idBooking).Include(b => b.Student).Include(c => c.Cooperative).ToList();
                    return View(BookingList);
                }
                else
                {
                    List<BookingLine> BookingLineList = db.BookingLine.ToList();
                    List<Booking> BookingList = new List<Booking>();
                    foreach (BookingLine element in BookingLineList)
                    {
                        if (BookingList.Where(b => b.IDBooking == element.IDBooking).SingleOrDefault() == null)
                        {
                            BookingList.Add(db.Booking.Where(b => b.IDBooking == element.IDBooking).Include(b => b.Student).Include(c => c.Cooperative).Single());
                        }
                    }
                    return View(BookingList);
                }
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginStudents", "Accounts");
        }

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


        public ActionResult Create()
        {
            if (AccountManagement.isConnected() != null && AccountManagement.getEstManager() == false)
            {
                ViewBag.IDCooperative = new SelectList(db.Cooperative, "IDCooperative", "Name");
                return View();
            }
            //Redirection vers la page de login si il tente d'accéder à la page 
            return RedirectToAction("LoginStudents", "Accounts");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingCoopViewModel book)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Book.Add(book);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(book);
        }

        // Méthode qui effectue la recherche du livre dans la BD
        [HttpPost]
        [ActionName("SearchBooks")]
        public ActionResult SearchBooks(string Value)
        {
            if (Value != "") // On vérifie si l'utilisateur à bel et bien entré quelque chose dans le champ
            {
                // On permet à l'utilisateur d'entrer tous les paramètre de la recherche dans un seul champs
                var Books = (from B in db.Book
                             join BA in db.BooksAuthors on B.IDBook equals BA.IDBook
                             join A in db.Author on BA.IDAuthor equals A.IDAuthor
                             join BC in db.BooksCopy on B.IDBook equals BC.IDBook
                             join C in db.Cooperative on BC.IDCooperative equals C.IDCooperative
                             join S in db.Student on BC.IDStudent equals S.IDStudent
                             join BS in db.BookState on BC.IDBookState equals BS.IDBookState
                             where BC.Available == 1 && (B.noISBN == Value || B.noUPC == Value || B.noEAN == Value || B.Title.Contains(Value) || S.FirstName + " " + S.LastName == Value || A.Name.Contains(Value))
                             select new { BC.IDBooksCopy, B.noISBN, B.Title, B.price, AuthorName = A.Name, S.FirstName, S.LastName, BS.Description, BS.PricePercentage,C.IDCooperative , CoopName = C.Name}).ToList();

                // S'il n'y a rien trouvé, on doit retourner une erreur. 
                if (Books.Count == 0)
                {
                    return View();
                }
                else
                {
                    //Récupérer les informations et les retourner en json
                    return Json(Books, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        // Permet d'aller chercher le copy du livre qui à été sélectionné. 
        [HttpPost]
        [ActionName("getBookCopy")]
        public ActionResult getBookCopy(string id)
        {
            int idBookCopy = Int32.Parse(id);
            var Books = (from BC in db.BooksCopy
                         join BA in db.BooksAuthors on BC.Book.IDBook equals BA.IDBook
                         join A in db.Author on BA.IDAuthor equals A.IDAuthor
                         join C in db.Cooperative on BC.IDCooperative equals C.IDCooperative
                         where BC.IDBooksCopy == idBookCopy
                         select new { BC.IDBooksCopy, BC.Book.noISBN, BC.Book.Title, BC.Student.FirstName, BC.Student.LastName, BC.BookState.IDBookState, BC.BookState.PricePercentage, BC.Book.price, AuthorName = A.Name, C.IDCooperative , CoopName = C.Name }).SingleOrDefault();

            //Récupérer les informations et les retourner en json
            IDLivreTrouver = idBookCopy;
            return Json(Books, JsonRequestBehavior.AllowGet);
        }



        // GET: BookDelivery/Delete/5
        public ActionResult Delete(int? id)
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
            return View(booking);
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

        // Effectue la réservation en BD
        public void CreateBooking(string id,string idCoop)
        {
            DateTime localDate = DateTime.Now;


            Booking booking = new Booking();
            BookingLine bookingline = new BookingLine();

            booking.BookingDate = localDate;
            booking.IDStudent = AccountManagement.getIDAccount();
            booking.IDManager = null; // À CHANGÉ
            booking.TradeConfirmation = false;
            booking.IDCooperative = Int32.Parse(idCoop);
            db.Booking.Add(booking);

            
            bookingline.IDBooksCopy =  Int32.Parse(id); // À Changé
            bookingline.BookingState = -1; // À 48h
            db.BookingLine.Add(bookingline);

            // On indique que le livre n'est plus disponible
            int idlivre = Int32.Parse(id);
            BooksCopy bookcopy =
                (from b in db.BooksCopy
                 where b.IDBooksCopy == idlivre
                 select b).SingleOrDefault(); 

            bookcopy.Available = 0;
            db.Entry(bookcopy).State = EntityState.Modified;

            Book book =
                (from b in db.Book
                 where b.IDBook == bookcopy.IDBook
                 select b).SingleOrDefault();



            // On sauvegarde en BD les modifs
            db.SaveChanges();
            //Transfert de livre entre coop
            if (booking.IDCooperative !=  bookcopy.IDCooperative)
            {
                //Ajout d'un transfert
                BooksCopyTransfer BCT = new BooksCopyTransfer();
                BCT.IDCooperativeFrom = bookcopy.IDCooperative;
                BCT.IDCooperativeTo = booking.IDCooperative;
                BCT.TransferConfirmation = false;
                BCT.TransferDate = null;

                db.BooksCopyTransfer.Add(BCT);
                db.SaveChanges();

                //Ajout de la ligne de transfert
                BooksCopyTransferLine BCTL = new BooksCopyTransferLine();
                BCTL.IDBooksCopy = bookcopy.IDBooksCopy;
                BCTL.IDBooksCopyTransfer = BCT.IDBooksCopyTransfer;
                BCTL.State = -1;
                db.BooksCopyTransferLine.Add(BCTL);
                db.SaveChanges();

                sendEmail(book, bookcopy, booking,true);
            }
            else
            {
                sendEmail(book, bookcopy, booking,false);
            }
        }

        public void sendEmail(Book b, BooksCopy bc, Booking bo,bool transfer)
        {
            string EmailContent = "";
            if(transfer)
            {
                EmailContent = "Votre réservation est complété. Un transfert de l'exemplaire réservé  à votre coopérative est nécéssaire. \n Un courriel vous sera envoyé lorsque votre coopérative aura recu votre exemplaire. \n Suite à la réception de votre exemplaire, un délai de 48H vous sera attribué.";
            }
            else
            {
                EmailContent = "Votre réservation est complété. SVP passer le récupérer d'ici 48h.";
            }
            EmailContent = EmailContent + "\n" + "------------------------------------------------------------------------------------------------------";
            EmailContent = EmailContent + " \n" + UtilResources.GetLabel("Titre") + ": " + b.Title;
            EmailContent = EmailContent + " \n";
            EmailContent = EmailContent + "------------------------------------------------------------------------------------------------------";

            int IDTemp = bo.IDStudent;
            Student s = db.Student.Where(stu => stu.IDStudent == IDTemp).FirstOrDefault();
            UtilResources.SendMail(s.Email, UtilResources.GetLabel("Réservation") + " #" + bo.IDBooking, UtilResources.GetLabel("Bonjour") + " " + s.FirstName + " " + s.LastName + ", \n\n" + EmailContent);
        }
    }    
}
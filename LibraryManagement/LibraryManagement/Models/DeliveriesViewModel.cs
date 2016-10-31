using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class DeliveriesViewModel
    {
       public Booking booking { get; set; }
       public List<BookingLine> bookingLineList { get; set; }
       public Student student { get; set; }

        //Temp en test
        public List<BooksCopy> booksCopyList { get; set; }
        public List<BookState> booksStateList { get; set; }
        public List<Book> bookList { get; set; }
    }
}
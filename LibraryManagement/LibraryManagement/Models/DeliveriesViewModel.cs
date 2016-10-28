using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class DeliveriesViewModel
    {
        Booking booking { get; set; }
        List<BookingLine> bookingLineList {get; set;}
        List<Booking> bookingList { get; set;}
    }
}
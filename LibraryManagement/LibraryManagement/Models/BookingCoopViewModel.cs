using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookingCoopViewModel
    {
        public Book book { get; set; }
        public Cooperative coop { get; set; }
    }
}
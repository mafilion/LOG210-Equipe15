using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookDeliveriesViewModel
    {
       public int idBookState { get; set; }
       public string noISBN { get; set; }
       public string author { get; set; }
       public string title { get; set; }
       public string student { get; set; }
       public BookState state { get; set; }
       public string price { get; set; }
       public Cooperative coop { get; set; }

    }


}
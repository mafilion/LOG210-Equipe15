using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookDeliveriesViewModel
    {
       public Book book { get; set; }
       public Author author { get; set; }
       public Student student { get; set; }
       public BookState state { get; set; }
       public BooksCopy copy { get; set; }

    }
}
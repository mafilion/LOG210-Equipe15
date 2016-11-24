using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BooksCopyTransferViewModel
    {
        public BooksCopyTransfer bct = new BooksCopyTransfer();
        public BooksCopyTransferLine bctl = new BooksCopyTransferLine();
        public Cooperative coopFrom = new Cooperative();
        public Cooperative coopTo = new Cooperative();
        public BooksCopy booksCopy = new BooksCopy();
        public Author aut = new Author();
    }
}
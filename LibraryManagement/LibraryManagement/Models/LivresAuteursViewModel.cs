using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement2.Models
{
    public class LivresAuteursViewModel
    {
        public Livre livre { get; set; }
        public Auteur auteur { get; set; }
        public LivresAuteurs livreAuteur { get; set; }
    }
}
using LibraryManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class LivresAuteursViewModel
    {
        public Livre livre { get; set; }
        public List<Auteur> ListAuteur { get; set; }
        public List<LivresAuteurs> ListlivreAuteur { get; set; }
    }
}
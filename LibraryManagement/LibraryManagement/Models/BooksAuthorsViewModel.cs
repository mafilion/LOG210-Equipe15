using LibraryManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BooksAuthorsViewModel
    {
        public Book book { get; set; }
        public List<Author> AuthorList { get; set; }
        public Author Aut { get; set; }
        public List<BooksAuthors> BooksAuthorsList { get; set; }
        public BookState bookState { get; set; }
        public Cooperative coop { get; set; }
    }
}
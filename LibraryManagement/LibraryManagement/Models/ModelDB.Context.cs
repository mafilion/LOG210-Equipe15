﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class libraryManagementEntities : DbContext
    {
        public libraryManagementEntities()
            : base("name=libraryManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cooperative> Cooperative { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BooksAuthors> BooksAuthors { get; set; }
        public virtual DbSet<BooksCopy> BooksCopy { get; set; }
        public virtual DbSet<BookState> BookState { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BooksCopy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BooksCopy()
        {
            this.BookingLine = new HashSet<BookingLine>();
            this.BooksCopyTransferLine = new HashSet<BooksCopyTransferLine>();
        }
    
        public int IDBooksCopy { get; set; }
        public int IDStudent { get; set; }
        public int IDBook { get; set; }
        public int IDBookState { get; set; }
        public int IDCooperative { get; set; }
        public int Available { get; set; }
    
        public virtual Book Book { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingLine> BookingLine { get; set; }
        public virtual BookState BookState { get; set; }
        public virtual Cooperative Cooperative { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksCopyTransferLine> BooksCopyTransferLine { get; set; }
    }
}

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
    
    public partial class BooksCopyTransfer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BooksCopyTransfer()
        {
            this.BooksCopyTransferLine = new HashSet<BooksCopyTransferLine>();
        }
    
        public int IDBooksCopyTransfer { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public bool TransferConfirmation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksCopyTransferLine> BooksCopyTransferLine { get; set; }
    }
}

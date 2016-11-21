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
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.BookingLine = new HashSet<BookingLine>();
        }
    
        public int IDBooking { get; set; }
        public int IDStudent { get; set; }
        public Nullable<int> IDManager { get; set; }
        public System.DateTime BookingDate { get; set; }
        public bool TradeConfirmation { get; set; }
        public Nullable<int> IDCooperative { get; set; }
    
        public virtual Manager Manager { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingLine> BookingLine { get; set; }
        public virtual Cooperative Cooperative { get; set; }
    }
}

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
    
    public partial class BooksCopyTransferLine
    {
        public int IDBooksCopyTransferLine { get; set; }
        public int IDBooksCopyTransfer { get; set; }
        public int IDBooksCopy { get; set; }
        public int State { get; set; }
        public Nullable<int> IDCooperativeFrom { get; set; }
        public Nullable<int> IDCooperativeTo { get; set; }
    
        public virtual BooksCopy BooksCopy { get; set; }
        public virtual BooksCopyTransfer BooksCopyTransfer { get; set; }
    }
}

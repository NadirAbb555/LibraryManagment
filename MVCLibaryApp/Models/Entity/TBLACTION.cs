//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCLibaryApp.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLACTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLACTION()
        {
            this.TBLFINE = new HashSet<TBLFINE>();
        }
    
        public int ID { get; set; }
        public Nullable<int> BOOK { get; set; }
        public Nullable<int> MEMBER { get; set; }
        public Nullable<int> PERSONAL { get; set; }
        public Nullable<System.DateTime> DELIVERYDATE { get; set; }
        public Nullable<System.DateTime> RETURNDATE { get; set; }
        public Nullable<System.DateTime> RETURNNOW { get; set; }
        public Nullable<bool> SD { get; set; }
        public Nullable<int> TotalDate { get; set; }
    
        public virtual TBLBOOK TBLBOOK { get; set; }
        public virtual TBLMEMBERS TBLMEMBERS { get; set; }
        public virtual TBLPERSONAL TBLPERSONAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLFINE> TBLFINE { get; set; }
    }
}

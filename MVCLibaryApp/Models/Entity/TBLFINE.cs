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
    
    public partial class TBLFINE
    {
        public int ID { get; set; }
        public Nullable<int> MEMBER { get; set; }
        public Nullable<System.DateTime> DELIVERYDATE { get; set; }
        public Nullable<System.DateTime> RETURNDATE { get; set; }
        public Nullable<decimal> FINE { get; set; }
        public Nullable<int> ACTION { get; set; }
    
        public virtual TBLACTION TBLACTION { get; set; }
        public virtual TBLMEMBERS TBLMEMBERS { get; set; }
    }
}
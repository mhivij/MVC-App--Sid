//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Orders_Engine_module_2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscountProductMap
    {
        public int MapID { get; set; }
        public int ProductID { get; set; }
        public int DiscountID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual Discount Discount { get; set; }
    }
}

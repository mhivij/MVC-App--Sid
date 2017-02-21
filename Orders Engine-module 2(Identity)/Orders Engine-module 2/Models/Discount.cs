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
    using System.Web.Mvc;

    public partial class Discount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discount()
        {
            this.DiscountProductMaps = new HashSet<DiscountProductMap>();
        }
    
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public int DiscountTypeID { get; set; }
        public decimal DiscountPercent { get; set; }
        public System.DateTime CreatedDate { get { return DateTime.Now; } set { } }
        public string CreatedBy { get { return "Admin"; } set { } }
        public IEnumerable<SelectListItem> discountTypename { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountProductMap> DiscountProductMaps { get; set; }
        public virtual DiscountType DiscountType { get; set; }
    }
}
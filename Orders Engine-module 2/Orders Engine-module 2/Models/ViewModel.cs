using System.Collections.Generic;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Models
{
    public class ViewModel
    {
        public IEnumerable<Discount> Discount { get; set; }
        public IEnumerable<DiscountType> DiscountType { get; set; }
        public IEnumerable<DiscountProductMap> DiscountProductMap { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SelectListItem> CartItem { get; set; }
    }
}
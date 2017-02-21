using Orders_Engine_module_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class Validations:Controller
    {
        public static SelectList productNameList;
        public static SelectList discountNameList;
        public static SelectList discountTypesNameList;
        Discount discountmodel = new Discount();
        ProductsEntities dbpro = new ProductsEntities();
        DiscountEntities db = new DiscountEntities();

        public DiscountProductMap GetDisToProListValue(DiscountProductMap DPM)
        {                                                                       
                                                                                                              //value        //Text
            DPM.Productname = new SelectList(dbpro.Products.Select(x => new { x.ProductName, x.ProductID }), "ProductID", "ProductName", "ProductName");

                                                                                                                //value           //Text
            DPM.discountname = new SelectList(db.Discounts.Select(x => new { x.DiscountName, x.DiscountID }), "DiscountID", "DiscountName", "DiscountName");
            return DPM;
        }

        public Discount GetDiscountTypeListValues(Discount discount)
        {
                                                                                                                                       //value           //Text
            discount.discountTypename = new SelectList(db.DiscountTypes.Select(x => new { x.DiscountTypeName, x.DiscountTypeID }), "DiscountTypeID", "DiscountTypeName");
            return discount;
        }
        public bool CheckIfUserIsLoggedIn()
        {
            bool login = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (login)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
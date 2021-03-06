﻿using Orders_Engine_module_2.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class HomeController : Controller
    {
        private ProductsEntities db = new ProductsEntities();
        DiscountEntities disdb = new DiscountEntities();
        static ViewModel vm = new ViewModel();
        CustomersController a = new CustomersController();

        public async Task<ActionResult> RenderImage(int id)
        {
            Product product = await db.Products.FindAsync(id);

            byte[] ShowImage = product.ProductImage;

            return File(ShowImage, "image/png");
        }

        public int PriceAfterTax(int productPrice, int TaxAmout)
        {
            int finalamount = (productPrice * TaxAmout)/100;
            finalamount = finalamount + productPrice;
            return finalamount;
        }


        // GET: Home
        public ActionResult Homepage(string Category)
        {
            
            try
            {
                if (Category != null)
                {
                    var p = db.ProductCategories.Where(x => x.ProductCategoryName == Category).Select(x => x.ProductCategoryID).FirstOrDefault();

                     vm.Products= db.Products.Where(x => x.ProductCategoryID == p).ToList();
                    if (vm.Products.Count() != 0)
                    {
                        return View(vm);
                    }
                    else
                    {
                        ViewBag.message = "Record not available";
                        return View();
                    }
                }
                else
                {
                    vm.Products = db.Products.Select(x => x).ToList();
                    vm.DiscountProductMap = disdb.DiscountProductMaps;
                    return View(vm);
                }
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View();
            }

        }

        // GET: Details
        public ActionResult DisplayProductDetails(int id)
        {
            try
            {
                ViewModel vm1 = new ViewModel();
                var discountid = vm.DiscountProductMap.Where(x => x.ProductID == id).Select(x => x.DiscountID).FirstOrDefault();
                vm1.Discount = disdb.Discounts.Where(x => x.DiscountID == discountid).ToList();
                vm1.Products = vm.Products.Where(x => x.ProductID == id);
                
                return View(vm1);
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View("Homepage");
            }
        }
        [HttpPost]                                 
        public ActionResult DisplayProductDetails([Bind(Include = "Products")]ViewModel model,string command)
        {
            
            if (command == "AddtoCart")
            {
                bool check = new Validations().CheckIfUserIsLoggedIn();
                try
                {
                    if (check)
                    {
                        vm.CartItem = model.Products;
                        ViewBag.Cart = "Successfully added to cart";
                        return View(model);
                    }

                    else
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
                catch
                {
                    TempData["Error"] = "There is no Internet connection";
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public ActionResult ViewCartItem()
        {
            try
            {
                bool check = new Validations().CheckIfUserIsLoggedIn();
                if (check)
                {
                    return View("ViewCartItem",vm);
                }

                else
                {
                    return RedirectToAction("Login", "Auth");
                }

            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View("Homepage");
            }
        }
    }
}
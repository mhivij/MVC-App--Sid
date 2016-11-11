using Orders_Engine_module_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class DiscountController : Controller
    {
        DiscountEntities db = new DiscountEntities();
        ProductsEntities dbpro = new ProductsEntities();

        Discount discountmodel = new Discount();
        DiscountProductMap DPM = new DiscountProductMap();

        // GET: Discount//Partial View
        public ActionResult DiscountOptions()
        {
            return View();
        }

        public ActionResult ViewDiscount()
        {
            return View();
        }

        // GET: Discount/Create
        public ActionResult CreateNewDiscount()
        {                                                                                                                               //value           //Text
            discountmodel.discountTypename = new SelectList(db.DiscountTypes.Select(x => new { x.DiscountTypeName,x.DiscountTypeID }),"DiscountTypeID", "DiscountTypeName");
            return View(discountmodel);
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult CreateNewDiscount([Bind(Include = "DiscountID,DiscountName,DiscountTypeID,DiscountPercent,CreatedDate,CreatedBy")] Discount discount)
        {
            try
            {                
                    db.Discounts.Add(discount);
                    db.SaveChanges();
                    return RedirectToAction("DiscountOptions");
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View(discount);
            }
        }

        // GET: DiscountType/Create
        public ActionResult CreateNewDiscountType()
        {
            return View();
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult CreateNewDiscountType([Bind(Include = "DiscountTypeID,DiscountTypeName,CreatedDate,CreatedBy")] DiscountType discountType)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var discount = db.DiscountTypes.Where(x => x.DiscountTypeName == discountType.DiscountTypeName).Select(x => x.DiscountTypeName);
                    if (discount == null)
                    {
                        db.DiscountTypes.Add(discountType);
                        db.SaveChanges();
                        return RedirectToAction(" DiscountOptions");
                    }
                    else
                    {
                        ViewBag.message = "Record already available";
                    }
                }
                return View(discountType);
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View(discountType);
            }
        }

        // GET: Discount/Create
        public ActionResult AddDiscountToProducts()
        {  
                                                                                                              //value           //Text
            DPM.Productname = new SelectList(dbpro.Products.Select(x => new { x.ProductName, x.ProductID }), "ProductID", "ProductName");

                                                                                                               //value           //Text
            DPM.discountname = new SelectList(db.Discounts.Select(x => new { x.DiscountName, x.DiscountID }), "DiscountID", "DiscountName");

            return View(DPM);
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult AddDiscountToProducts([Bind(Include = "MapID,ProductID,DiscountID,CreatedDate,CreatedBy")] DiscountProductMap DisPro)
        {
            try
            {
                db.DiscountProductMaps.Add(DisPro);
                db.SaveChanges();
                return RedirectToAction("DiscountOptions");
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View();
            }
        }






        // GET: Discount/Edit/5
        /* public ActionResult Edit(int id)
         {
             return View();
         }

         // POST: Discount/Edit/5
         [HttpPost]
         public ActionResult Edit(int id, FormCollection collection)
         {
             try
             {
                 // TODO: Add update logic here

                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }
         }*/

        // GET: Discount/Delete/5
        public ActionResult DeleteDiscount(int id)
        {
            return View();
        }

        // POST: Discount/Delete/5
        [HttpPost]
        public ActionResult DeleteDiscount(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Discount/Delete/5
        public ActionResult DeleteDiscountType(int id)
        {
            return View();
        }

        // POST: Discount/Delete/5
        [HttpPost]
        public ActionResult DeleteDiscountType(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

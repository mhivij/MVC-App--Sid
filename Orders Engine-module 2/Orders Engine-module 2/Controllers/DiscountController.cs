using Orders_Engine_module_2.Models;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class DiscountController : Controller
    {
        DiscountEntities db = new DiscountEntities();
        // GET: Discount
        public ActionResult DiscountOptions()
        {
            return View();
        }


        // GET: Discount/Create
        public ActionResult CreateNewDiscount()
        {
            return View();
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult CreateNewDiscount(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Discount/Create
        public ActionResult CreateNewDiscountType()
        {
            return View();
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult CreateNewDiscountType([Bind(Include = "DiscountTypeID,DiscountTypeName,CreatedDate,CreatedBy")] DiscountType productType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = db.DiscountTypes.Where(x => x.ProductCategoryName == productcategory.ProductCategoryName).Select(x => x.ProductCategoryName);
                    if (product == null)
                    {
                        db.DiscountTypes.Add(productType);
                        db.SaveChanges();
                        return RedirectToAction(" DiscountOptions");
                    }
                    else
                    {
                        ViewBag.message = "Record already available";
                    }
                }
                return View("DiscountOptions");
            }
            catch
            {
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

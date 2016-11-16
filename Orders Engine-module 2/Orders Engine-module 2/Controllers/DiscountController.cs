using Orders_Engine_module_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class DiscountController : Controller
    {
        DiscountEntities db = new DiscountEntities();
        ProductsEntities dbpro = new ProductsEntities();

        // GET:Product name from ID 
        public ActionResult ConvertProductIdToName(int id)
        {
           var prodname = dbpro.Products.Where(x=>x.ProductID==id).Select(x => x.ProductName).Single();

            return Content(prodname);
        }

        // GET:Discount name from ID 
        public ActionResult ConvertDiscountIdToName(int id)
        {
            var Discountname = db.Discounts.Where(x => x.DiscountID == id).Select(x => x.DiscountName).First();

            return Content(Discountname);
        }

        // GET: Discount//Partial View
        public ActionResult DiscountOptions()
        {
            return View();
        }

        public ActionResult ViewDiscount()
        {
            ViewModel vm = new ViewModel();
            vm.Discount= db.Discounts;
            vm.DiscountType=db.DiscountTypes;
            vm.DiscountProductMap=db.DiscountProductMaps;
            return View(vm);
        }

        // GET: Discount/Create
        public ActionResult CreateNewDiscount()
        {
            Discount discount = new Discount();
            discount = new Validations().GetDiscountTypeListValues(discount);
            return View(discount);
        }

        // POST: Discount/Create
        [HttpPost]
        public ActionResult CreateNewDiscount([Bind(Include = "DiscountID,DiscountName,DiscountTypeID,DiscountPercent,CreatedDate,CreatedBy")] Discount discount)
        {
            try
            {                
                    db.Discounts.Add(discount);
                    db.SaveChanges();
                    return RedirectToAction("ViewDiscount");
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
                        return RedirectToAction("ViewDiscount");
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
            DiscountProductMap DPM = new DiscountProductMap();
            DPM= new Validations().GetDisToProListValue(DPM);

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
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View();
            }
        }



        // GET: Discount/Edit/5
        public ActionResult EditDiscount(int id)
        {
            try
            {
                Discount discount = db.Discounts.Find(id);
                discount=new Validations().GetDiscountTypeListValues(discount);
                
                if (discount == null)
                {
                    return HttpNotFound();
                }
                return View(discount);
            }
            catch
            {
                return RedirectToAction("ViewDiscount");
            }
        }

        //POST: Discount/Edit/5
         [HttpPost]
        public ActionResult EditDiscount(int id, Discount discount)
        {
            try
            {
                discount = db.Discounts.Find(id);
                UpdateModel(discount);
                db.SaveChanges();
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View(discount);
            }
        }

        // GET: Discount/Edit/5
        public ActionResult EditDiscountProductsMap(int id)
        {
            try
            {
                DiscountProductMap DPM = new DiscountProductMap();
                DPM= db.DiscountProductMaps.Find(id);
                DPM = new Validations().GetDisToProListValue(DPM);

                if (DPM == null)
                {
                    return HttpNotFound();
                }
                return View(DPM);
            }
            catch
            {
                return RedirectToAction("ViewDiscount");
            }
        }

        //POST: Discount/Edit/5
        [HttpPost]
        public ActionResult EditDiscountProductsMap(int id, DiscountProductMap DPM)
        {
            try
            {
                DPM = db.DiscountProductMaps.Find(id);
                UpdateModel(DPM);
                db.SaveChanges();
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View(DPM);
            }
        }






        // GET: Discount/Delete/5
        public ActionResult DeleteDiscount(int id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Discount discount = db.Discounts.Find(id);
                if (discount == null)
                {
                    return HttpNotFound();
                }
                return View(discount);
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View();
            }
        }

        // POST: Discount/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDiscount(int id, Discount discount)
        {
            try
            {
                // TODO: Add delete logic here
                discount = db.Discounts.Find(id);
                db.Discounts.Remove(discount);
                db.SaveChanges();
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return base.View(discount);
            }
        }

        // GET: Discount/Delete/5
        public ActionResult DeleteDiscountType(int id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DiscountType discounttype = db.DiscountTypes.Find(id);
                if (discounttype == null)
                {
                    return HttpNotFound();
                }
                return View(discounttype);
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View();
            }
        }

        // POST: Discount/Delete/5
        [HttpPost]
        public ActionResult DeleteDiscountType(int id, DiscountType discounttype)
        {
            try
            {
                // TODO: Add delete logic here
                discounttype = db.DiscountTypes.Find(id);
                db.DiscountTypes.Remove(discounttype);
                db.SaveChanges();
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return base.View();
            }
        }

        // GET: Discount/Delete/5
        public ActionResult DeleteDiscountProductsMap(int id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DiscountProductMap DPM = db.DiscountProductMaps.Find(id);
                if (DPM == null)
                {
                    return HttpNotFound();
                }
                return View(DPM);
            }
            catch
            {
                return RedirectToAction("ViewDiscount");
            }
        }

        // POST: Discount/Delete/5
        [HttpPost]
        public ActionResult DeleteDiscountProductsMap(int id, DiscountProductMap DPM)
        {
            try
            {
                // TODO: Add delete logic here
                DPM = db.DiscountProductMaps.Find(id);
                db.DiscountProductMaps.Remove(DPM);
                db.SaveChanges();
                return RedirectToAction("ViewDiscount");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return base.View(DPM);
            }
        }
    }
}

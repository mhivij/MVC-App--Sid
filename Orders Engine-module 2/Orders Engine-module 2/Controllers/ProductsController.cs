using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Orders_Engine_module_2.Models;
using System.IO;
using System.Threading.Tasks;

namespace Orders_Engine_module_2.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsEntities db = new ProductsEntities();
        static List<ProductCategory> prodlist;
        static List<SelectListItem> ProdCateList = new List<SelectListItem>();
        static byte[] img;

        // GET:Byte to Image 
        public async Task<ActionResult> RenderImage(int id)
        {
            Product product = await db.Products.FindAsync(id);

            byte[] ShowImage = product.ProductImage;

            return File(ShowImage, "image/png");
        }

        // GET: Products
        public ActionResult ViewProducts()
        {
            var products = db.Products.Include(p => p.ProductCategory);

            //Create DropDownList
            if(db.ProductCategories.ToList().Count>0)
            {
                prodlist = db.ProductCategories.ToList();
                foreach (var x in prodlist)
                {
                    ProdCateList.Add(new SelectListItem { Text = x.ProductCategoryName.ToString(), Value = x.ProductCategoryName.ToString() });
                }
            }
            return View(products.ToList());
        }

        // GET: Products/Create
        public ActionResult InsertNewProduct()
        {
            ViewData["ProductCategoryName"] = ProdCateList;
            //ViewBag.ProductID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertNewProduct([Bind(Include = "ProductID,ProductName,ProductCategoryID,ProductDescription,ProductImage,IsTaxable,TaxAmout,CreatedDate,CreatedBy,ProductPrice ")] Product product)
        {
            if (ModelState.IsValid)
            {
                //If the selected Product Category Name in drop down list matches with the Product Category name in product category table then its corresponding Product id is saved in product id column of products table.
                foreach (var x in prodlist)
                {
                    if(Request["ProductCategoryName"].ToString() == x.ProductCategoryName)
                    {
                        product.ProductCategoryID = x.ProductCategoryID;
                    }
                }

                    HttpPostedFileBase uploadImage = Request.Files["uploadImage"];
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    product.ProductImage = imageData;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ViewProducts");
            }

            ViewBag.ProductID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryName", product.ProductID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult EditProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData["ProductCategoryName"] = ProdCateList;

            Product product = db.Products.Find(id);
            img = product.ProductImage;
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryName", product.ProductID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductDetails([Bind(Include = "ProductID,ProductName,ProductCategoryID,ProductDescription,ProductImage,IsTaxable,TaxAmout,CreatedDate,CreatedBy,ProductPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                foreach (var x in prodlist)
                {
                    if (Request["ProductCategoryName"].ToString() == x.ProductCategoryName)
                    {
                        product.ProductCategoryID = x.ProductCategoryID;
                    }
                }
                product.ProductImage = img;
                db.SaveChanges();
                return RedirectToAction("ViewProducts");
            }
            ViewBag.ProductID = new SelectList(db.ProductCategories, "ProductCategoryID", "ProductCategoryName", product.ProductID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ViewProducts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

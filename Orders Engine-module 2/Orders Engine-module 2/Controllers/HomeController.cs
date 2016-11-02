using Orders_Engine_module_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

using System.Web.Mvc;
using System.IO;

namespace Orders_Engine_module_2.Controllers
{
    public class HomeController : Controller
    {
        private ProductsEntities db = new ProductsEntities();
        int id;
        public async Task<ActionResult> RenderImage(int id)
        {
            Product product = await db.Products.FindAsync(id);

            byte[] ShowImage = product.ProductImage;

            return File(ShowImage, "image/png");
        }
        public ActionResult PartialProducts(string Category)
        {
            var p = db.ProductCategories.Where(x => x.ProductCategoryName== Category).ToList().FirstOrDefault();
            var products = db.Products.Select(x => x).ToList().Where(x=>x.ProductCategoryID == p.ProductCategoryID);

            return View(products);
        }
        // GET: Home
        public ActionResult Homepage()
        {
            var products = db.Products.Select(x => x).ToList();
           
            return View(products);
        }

        // GET: Details
        public ActionResult DisplayProductDetails(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
    }
}
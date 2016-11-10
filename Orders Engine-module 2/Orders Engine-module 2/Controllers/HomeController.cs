using Orders_Engine_module_2.Models;

using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class HomeController : Controller
    {
        private ProductsEntities db = new ProductsEntities();

        public HomeController()
        {
            Session["session"] = false;
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Product product = await db.Products.FindAsync(id);

            byte[] ShowImage = product.ProductImage;

            return File(ShowImage, "image/png");
        }

        // GET: Home
        public ActionResult Homepage(string Category)
        {
            
            try
            {
                if (Category != null)
                {
                    var p = db.ProductCategories.Where(x => x.ProductCategoryName == Category).Select(x => x.ProductCategoryID).FirstOrDefault();

                    var products = db.Products.Where(x => x.ProductCategoryID == p).ToList();
                    if (products.Count != 0)
                    {
                        return View(products);
                    }
                    else
                    {
                        ViewBag.message = "Record not available";
                        return View();
                    }
                }
                else
                {
                    var products = db.Products.Select(x => x).ToList();
                    return View(products);
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
                var product = db.Products.Find(id);
                return View(product);
            }
            catch
            {
                ViewBag.message = "There is no Internet connection";
                return View("Homepage");
            }
        }
    }
}
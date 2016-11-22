using System.Linq;
using Orders_Engine_module_2.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Orders_Engine_module_2.Controllers
{
    public class CustomersController : Controller
    {
        Entities custdb = new Entities();
        ViewModel vm= new ViewModel();

        // GET: Customers
        public ActionResult ViewRecords()
        {
            try
            {
                var cust = custdb.Customers.Select(x => x).ToList();
                return View(cust);
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View();
            }
        }

        // GET: Customers/Create
        public ActionResult Insert()
        {
            return View(new Customer());
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Insert([Bind(Include = "FirstName,MiddleName,LastName,Company,CustomerTypeID,CustomerStatusID, Email, Phone, MainAddress1, MainAddress2, MainAddress3, MainCity, MainState, MainZip, MainCountry, MailAddress1, MailAddress2, MailAddress3, MailCity, MailState, MailZip, MailCountry,CanLogin,LoginName,BirthDate,CurrencyCode,LanguageID,Gender,TaxCode,TaxCodeTypeID,IsSalesTaxExempt,SalesTaxCode,IsEmailSubscribed,Notes,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")]Customer customer)
        {

           if (ModelState.IsValid)
             {
                try
                {
                    // TODO: Add insert logic here
                    custdb.Customers.Add(customer);
                    custdb.SaveChanges();
                    return RedirectToAction("ViewRecords");
                }
                catch
                {
                    TempData["Error"] = "There is no Internet connection";
                    return View(new Customer());
                }
            }
           else
            {
                TempData["Error"] = " Model state is false";
                return View(new Customer());
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Customer cust = custdb.Customers.Find(id);
                Session["Customer"] = cust;
                return View(cust);
            }
            catch
            {
                return RedirectToAction("ViewRecords");
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var cust = custdb.Customers.Find(id);
                UpdateModel(cust);
                custdb.SaveChanges();
                return RedirectToAction("ViewRecords");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View(customer);
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var cust = custdb.Customers.Find(id);
                return View(cust);
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return View();
            }
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var cust = custdb.Customers.Where(x => x.CustomerID == id).SingleOrDefault();
                custdb.Customers.Remove(cust);
                custdb.SaveChanges();
                return RedirectToAction("ViewRecords");
            }
            catch
            {
                TempData["Error"] = "There is no Internet connection";
                return base.View();
            }
        }

        public ActionResult AddToCart(ViewModel model)
        {
            bool check = new Validations().CheckIfUserIsLoggedIn();
            try
            {
                if (check)
                {
                    vm.CartItem = model.Products;
                    ViewBag.Cart = "Successfully added to cart";
                    return View("DisplayProductDetails", "Home", model);
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
    }
}

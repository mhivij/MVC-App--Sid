using Orders_Engine_module_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orders_Engine_module_2.Controllers
{
    public class OrdersHistoryController : Controller
    {
        OrdersEntities orderdb = new OrdersEntities();
        // GET: OrdersHistory
        public ActionResult ViewOrders()
        {
            var cust = orderdb.OrderHistories.Select(x => x).ToList();
            return View(cust.ToList());
        }

        // GET: OrdersHistory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdersHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersHistory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: OrdersHistory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdersHistory/Edit/5
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
        }

        // GET: OrdersHistory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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

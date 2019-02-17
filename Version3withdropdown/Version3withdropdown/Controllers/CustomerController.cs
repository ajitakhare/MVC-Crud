using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Version3withdropdown.Models;

namespace Version3withdropdown.Controllers
{
    public class CustomerView
    {
        public int CId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

    }
    public class CustomerController : Controller
    {
        ProductSoldEntities dbobj = new ProductSoldEntities();
        // GET: Customer
        public ActionResult CIndex()
        {
            return View();
        }
        public JsonResult GetAllCustomers()
        {
            var customers = dbobj.Customers.Select(x => new
            {
                CId = x.CId,
                CustomerAddress = x.CustomerAddress,
              
                CustomerName = x.CustomerName
            }).ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerView cus)
        {
            var c = new Customer { CId = cus.CId, CustomerAddress = cus.CustomerAddress,CustomerName = cus.CustomerName };
            dbobj.Customers.Add(c);
            dbobj.SaveChanges();

            return Json(c, JsonRequestBehavior.AllowGet);
            //return Json(new { Success = true });
        }
        public JsonResult GetbyID(int ID)
        {
            var Customer = dbobj.Customers.FirstOrDefault(x => x.CId == ID);
            return Json(Customer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateCustomer(Customer cus)
        {
            dbobj.Entry(cus).State = EntityState.Modified;
            dbobj.SaveChanges();
            return Json(cus, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveCustomer(int ID)
        {
            Customer customer = dbobj.Customers.Find(ID);
            dbobj.Customers.Remove(customer);
            return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbobj.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
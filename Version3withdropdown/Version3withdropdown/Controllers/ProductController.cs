using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Version3withdropdown.Models;

namespace Version3withdropdown.Controllers
{
    public class ProductView
    {
        public int PId { get; set; }
        public string ProductName { get; set;}
        public int ProductPrice { get; set; }
    }
    public class ProductController : Controller
    {
        ProductSoldEntities dbobj = new ProductSoldEntities();
        // GET: Product
        public ActionResult PIndex()
        {
            return View();
        }
        public JsonResult GetAllProducts()
        {
            var product = dbobj.Products.Select(x=>new
            {
                PId = x.PId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice
            }).ToList();
            return Json(product, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult AddProduct(ProductView p)
        {
            var product = new Product { PId = p.PId, ProductName = p.ProductName, ProductPrice = p.ProductPrice };
            dbobj.Products.Add(product);
            return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByID(int id)
        {
            var product = dbobj.Products.FirstOrDefault(x => x.PId == id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateProduct(Product product)
        {
            dbobj.Entry(product).State = EntityState.Modified;
            dbobj.SaveChanges();
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveProduct(int id)
        {
            Product product = dbobj.Products.Find(id);
            dbobj.Products.Remove(product);
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
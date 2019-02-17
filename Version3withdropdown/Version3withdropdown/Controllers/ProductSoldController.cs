using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Version3withdropdown.Models;

namespace Version3withdropdown.Controllers
{
    public class ProductSoldView
    {
        public int PSId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
    }
    public class ProductSoldController : Controller
    {
        ProductSoldEntities dbobj = new ProductSoldEntities();
        // GET: ProductSold
        public ActionResult PSIndex()
        {
            return View();
        }
        public JsonResult GetAllProductsold()
        {
            var data = dbobj.ProductSolds.Select(x => new
            {
                PSId = x.PSId,
                CustomerId = x.CustomerId,
                Customer = x.Customer.CustomerName,
                ProductId = x.ProductId,
                Product = x.Product.ProductName,
                SroreId = x.StoreId,
                Store = x.Store.StoreName,
             
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddProductSold(ProductSoldView ps)
        {
            var productdsold = new ProductSold { CustomerId = ps.CustomerId, ProductId = ps.ProductId, StoreId = ps.StoreId};
            dbobj.ProductSolds.Add(productdsold);
            dbobj.SaveChanges();

            return Json(dbobj.SaveChanges(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int id)
        {
            var ProductSold = dbobj.ProductSolds.Where(x => x.PSId ==id).Select(x => new
            {
                PSId = id,
                CustomerId = x.CustomerId,
                StoreId = x.StoreId,
                ProductId = x.ProductId
            }).FirstOrDefault();
            return Json(ProductSold, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateProductSold(ProductSold psold)
        {
            dbobj.Entry(psold).State = EntityState.Added;
            dbobj.SaveChanges();
            return Json(psold, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveProductSold(int id)
        {
            ProductSold psold = dbobj.ProductSolds.Find(id);
            dbobj.ProductSolds.Remove(psold);
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
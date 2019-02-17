using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Version3withdropdown.Models;

namespace Version3withdropdown.Controllers
{
    public class StoreView
    {
        public int SId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
    }
    public class StoreController : Controller
    {
        ProductSoldEntities dbobj = new ProductSoldEntities();
        // GET: Store
        public ActionResult SIndex()
        {
            return View();
        }
        public JsonResult GetAllStores()
        {
            var store = dbobj.Stores.Select(x => new
            {
                SId = x.SId,
                StoreName = x.StoreName,
                StoreAddress = x.StoreAddress
            }).ToList();
            return Json(store, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddStore(StoreView s)
        {
            var store = new Store
            {
                SId = s.SId,
                StoreName = s.StoreName,
                StoreAddress = s.StoreAddress
            };
            dbobj.Stores.Add(store);
            return Json(dbobj.SaveChanges(),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetByID(int id)
        {
            var store = dbobj.Stores.FirstOrDefault(x => x.SId == id);
            return Json(store, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateStore(Store store)
        {
            dbobj.Entry(store).State = System.Data.Entity.EntityState.Modified;
            dbobj.SaveChanges();
            return Json(store, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveStore(int id)
        {
            Store store = dbobj.Stores.Find(id);
            dbobj.Stores.Remove(store);
            dbobj.SaveChanges();
            return Json(store, JsonRequestBehavior.AllowGet);

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
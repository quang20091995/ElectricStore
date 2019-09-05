using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class ProductManageController : Controller
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();
        public ProductManageController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        // GET: ProductManage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllProducts()
        {
            List<Product> list_products = context.Products.ToList();
            return Json(list_products, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();

        public CategoryController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        // GET: Category
        public ActionResult Index(string category_name)
        {
            return View();
        }

        public JsonResult SearchByCategory(string category_name)
        {
            // Search products by category
            List<Product> result = context.Products.Join(
                context.Categories,
                x => x.CategoryId,
                y => y.CategoryId,
                (x, y) => new { x = x, y = y }).Where(n => n.y.CategoryName.Equals(category_name)).Select(x => x.x).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
using ElectricStore.Common;
using ElectricStore.Models.Element;
using ElectricStore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class SearchController : Controller
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();

        public SearchController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }


        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Search(string keyword)
        {
            List<Product> result = new List<Product>();

            // Search products by name
            List<Product> name_by_result = context.Products.Where(x => x.ProductName.Contains(keyword)).ToList();

            result.AddRange(name_by_result);

            // Search products by category
            List<Product> category_by_result = context.Products.Join(
                context.Categories,
                x => x.CategoryId,
                y => y.CategoryId,
                (x, y) => new { x = x, y = y }).Where(n => n.y.CategoryName.Contains(keyword)).Select(n => n.x).ToList();

            result.AddRange(category_by_result);

            // Search products by company
            //List<Product> company_by_result = context.Products.Join(
            //    context.Companies,
            //    x => x.ManufactureId,
            //    y => y.ManufactureId,
            //    (x, y) => new { x = x, y = y }).Where(n => n.y.Manufacture.Contains(keyword)).Select(n=>n.x).ToList();

            //result.AddRange(company_by_result);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchAdvanced(SearchAdvancedRequest search_request)
        {
            var result = context.Products.Join(context.ProductDetails, product => product.ProductId, product_detail => product_detail.ProductId, (product, product_detail) => new { product = product, product_detail = product_detail }).ToList();

            if(result != null)
            {
                foreach (var condition in search_request.Company)
                {
                    result = result.Where(x => x.product.Company.Equals(condition)).ToList();
                }

                CPUType cpu_type = new CPUType();
                foreach (var condition in search_request.CPU)
                {
                    result = result.Where(x => x.product_detail.Speed <= cpu_type.filterByCPU(condition).Max && x.product_detail.Speed >= cpu_type.filterByCPU(condition).Min).ToList();
                }

                foreach (var condition in search_request.Memory)
                {
                    result = result.Where(x => x.product_detail.RAM.Equals(condition)).ToList();
                }

                foreach (var condition in search_request.Disc)
                {
                    result = result.Where(x => x.product_detail.Disc.Equals(condition)).ToList();
                }

                Capacity capacity = new Capacity();
                foreach (var condition in search_request.Capacity)
                {
                    result = result.Where(x => x.product_detail.Capacity <= capacity.filterByCapacity(condition).Max && x.product_detail.Capacity >= capacity.filterByCapacity(condition).Min).ToList();
                }

                Monitorsize monitorsize = new Monitorsize();
                foreach (var condition in search_request.Monitorsize)
                {
                    result = result.Where(x => x.product_detail.Monitorsize <= monitorsize.filterByMonitorsize(condition).Max && x.product_detail.Monitorsize >= monitorsize.filterByMonitorsize(condition).Min).ToList();
                }

                foreach (var condition in search_request.Core)
                {
                    result = result.Where(x => x.product_detail.Core.Equals(condition)).ToList();
                }
            }
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
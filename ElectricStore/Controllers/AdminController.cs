using Catel.Linq;
using ElectricStore.Logic;
using ElectricStore.Logic.LogicExtension;
using ElectricStore.Models.Request;
using ElectricStore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();
        private readonly IPageLogic<ProductElement> ipage_logic = new PageLogic<ProductElement>();
        private readonly IProductDetailRepository<ProductDetail> iproduct_detail_repository = new IProductDetailRepository<ProductDetail>();
        private readonly int PAGE_SIZE = 6;

        public AdminController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageNews()
        {
            return View();
        }

        public ActionResult ManageProducts()
        {
            return View();
        }
        
        public JsonResult GetProductsByPagination(int current_page)
        {
            AdminProductResponse response = new AdminProductResponse();

            int page_size = PAGE_SIZE;
            int product_count = GetProduct().Count;

            response.ProductList = ipage_logic.paginate(GetProduct(), current_page, page_size);
            response.CurrentPage = current_page;

            double number = product_count / page_size;
            int page_total = (int)(Math.Ceiling(number));

            response.TotalPage = page_total;
            response.PageSize = page_size;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public List<ProductElement> GetProduct()
        {
            var products = context.Products.Join(
                    context.Categories,
                    pro => pro.CategoryId,
                    cat => cat.CategoryId,
                    (pro, cat) => new { pro = pro, cat = cat })
                .Join(
                    context.Companies,
                    pro => pro.pro.ManufactureId,
                    manu => manu.ManufactureId,
                    (pro1, manu) => new { pro = pro1, manu = manu }).Select(
                    product => new ProductElement{
                        ProductId = product.pro.pro.ProductId,
                        ProductName = product.pro.pro.ProductName,
                        CategoryName = product.pro.cat.CategoryName,
                        Manufacture = product.manu.Manufacture,
                        ProductPrice = product.pro.pro.ProductPrice,
                        StockStatus = product.pro.pro.StockStatus,
                        ProductImage = product.pro.pro.ProductImage
                    }
                );

            List<ProductElement> product_list = products.ToList<ProductElement>();
            return product_list;   
        }

        public JsonResult GetCategory()
        {
            List<Category> list_category = context.Categories.ToList();
            return Json(list_category, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompany()
        {
            List<Company> list_company = context.Companies.ToList();
            return Json(list_company, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailProduct()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddProduct(AddProductRequest parameter)
        {

            return Json(200, JsonRequestBehavior.AllowGet);
        }
    }
}
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
        private readonly IProductRepository iproduct_repository = new ProductRepository();
        private readonly IProductDetailRepository iproduct_detail_repository = new ProductDetailRepository();
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
            ModelState.Remove("Monitorsize");
            if (!ModelState.IsValid) {
                var validation_errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, validation_errors});
            }

            Product product_parameter = new Product();
            product_parameter.ProductName = parameter.ProductName;
            product_parameter.CategoryId = parameter.CategoryId;
            product_parameter.ManufactureId = parameter.ManufactureId;
            product_parameter.ProductPrice = parameter.ProductPrice;
            product_parameter.StockStatus = parameter.StockStatus;

            iproduct_repository.Insert(product_parameter);

            int created_product_id = context.Products.First(x => x.ProductName.Equals(parameter.ProductName)).ProductId;

            

            bool check_having_detail = iproduct_repository.CheckProductHavingDetail(created_product_id);

            if (check_having_detail)
            {
                
            }

            return Json(new { success = true, status = false, message = "Tạo mới sản phẩm thành công! Xin vui lòng tạo thông tin chi tiết sản phẩm" });
        }

        public JsonResult AddProductDetail(AddProductDetailRequest parameter)
        {
            if (ModelState.IsValid)
            {
                ProductDetail product_detail_parameter = new ProductDetail();
                product_detail_parameter.ProductId = parameter.ProductId;
                product_detail_parameter.Microprocessor = parameter.Microprocessor;
                product_detail_parameter.Speed = parameter.Speed;
                product_detail_parameter.Graphics = parameter.Graphics;
                product_detail_parameter.RAM = parameter.RAM;
                product_detail_parameter.Capacity = parameter.Capacity;
                product_detail_parameter.Hardware = parameter.Hardware;
                product_detail_parameter.Monitor = parameter.Monitor;
                product_detail_parameter.Monitorsize = parameter.Monitorsize;
                product_detail_parameter.Operation = parameter.Operation;
                product_detail_parameter.Color = parameter.Color;
                product_detail_parameter.Connection = parameter.Connection;
                product_detail_parameter.Gate = parameter.Gate;
                product_detail_parameter.Webcam = parameter.Webcam;
                product_detail_parameter.Recognition = parameter.Recognition;
                product_detail_parameter.Battery = parameter.Battery;
                product_detail_parameter.Size = parameter.Size;
                product_detail_parameter.Weight = parameter.Weight;
                product_detail_parameter.Description = parameter.Description;
                product_detail_parameter.Core = parameter.Core;
                product_detail_parameter.Disc = parameter.Disc;

                iproduct_detail_repository.Insert(product_detail_parameter);
            }
            
            return Json(new { success = true, status = false, message = "Tạo mới sản phẩm thành công" });
        } 

        public ActionResult EditProduct(int product_id)
        {
            return View();
        }
    }
}
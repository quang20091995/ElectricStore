using Catel.Linq;
using ElectricStore.Logic;
using ElectricStore.Logic.LogicExtension;
using ElectricStore.Models;
using ElectricStore.Models.Element;
using ElectricStore.Models.Request;
using ElectricStore.Models.Response;
using ElectricStore.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IProductImageRepository iproduct_image_repository = new ProductImageRepository();
        private readonly IImageResolve iimage_resolve;
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
                    product => new ProductElement
                    {
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

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult ProductDetail(int product_id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddProductApi(ProductRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                var validation_errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, validation_errors });
            }

            Product product_parameter = new Product();
            product_parameter.ProductName = parameter.ProductName;
            product_parameter.CategoryId = parameter.CategoryId;
            product_parameter.ManufactureId = parameter.ManufactureId;
            product_parameter.ProductPrice = parameter.ProductPrice;
            product_parameter.StockStatus = parameter.StockStatus;

            iproduct_repository.Insert(product_parameter);

            int created_product_id = context.Products.First(x => x.ProductName.Equals(parameter.ProductName)).ProductId;

            return Json(new { success = true, type = "add-product", message = Properties.ProductResources.CREATE_PRODUCT_SUCCESS, id = created_product_id });
        }

        [HttpPost]
        public JsonResult AddProductDetailApi(AddProductDetailRequest parameter)
        {
            ModelState.Remove("Monitorsize");
            if (!ModelState.IsValid)
            {
                var validation_errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, validation_errors });
            }

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

            return Json(new { success = true, type = "add-detail", message = String.Format(Properties.ProductResources.CREATE_PRODUCT_DETAIL_SUCCESS, parameter.ProductId) });
        }

        public ActionResult EditProduct(int product_id)
        {
            return View();
        }

        public JsonResult DeleteProduct(int product_id)
        {
            bool check_delete_product = iproduct_repository.Delete(product_id);
            if (check_delete_product)
            {
                bool check_delete_product_detail = iproduct_detail_repository.Delete(product_id);
                return Json(new { message = Properties.ProductResources.DELETE_PRODUCT_SUCCESS }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { message = Properties.ProductResources.DELETE_PRODUCT_FAILURE }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAlbum(int product_id)
        {
            List<Album> images = iproduct_image_repository.GetAllImages(product_id);
            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailProduct(int product_id)
        {
            ProductFullInfoResponse product_full_info = new ProductFullInfoResponse();

            Product product = iproduct_repository.GetById(product_id);

            product_full_info.ProductId = product.ProductId;
            product_full_info.ProductName = product.ProductName;
            product_full_info.CategoryName = product.Category.CategoryName;
            product_full_info.ManufactureName = product.Company.Manufacture;
            product_full_info.ProductPrice = product.ProductPrice;
            product_full_info.ProductImage = product.ProductImage;
            product_full_info.StockStatus = product.StockStatus;

            ProductDetail product_detail = iproduct_detail_repository.GetById(product_id);

            product_full_info.ProductId = product_detail.ProductId;
            product_full_info.Microprocessor = product_detail.Microprocessor;
            product_full_info.Speed = product_detail.Speed;
            product_full_info.Graphics = product_detail.Graphics;
            product_full_info.RAM = product_detail.RAM;
            product_full_info.Capacity = product_detail.Capacity;
            product_full_info.Hardware = product_detail.Hardware;
            product_full_info.Monitor = product_detail.Monitor;
            product_full_info.Monitorsize = product_detail.Monitorsize;
            product_full_info.Operation = product_detail.Operation;
            product_full_info.Color = product_detail.Color;
            product_full_info.Connection = product_detail.Connection;
            product_full_info.Gate = product_detail.Gate;
            product_full_info.Webcam = product_detail.Webcam;
            product_full_info.Recognition = product_detail.Recognition;
            product_full_info.Battery = product_detail.Battery;
            product_full_info.Size = product_detail.Size;
            product_full_info.Weight = product_detail.Weight;
            product_full_info.Description = product_detail.Description;
            product_full_info.Core = product_detail.Core;
            product_full_info.Disc = product_detail.Disc;

            return Json(product_full_info, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateProduct(ProductFullInfoRequest parameter)
        {
            if (ModelState.IsValid)
            {
                Product product_element = new Product();
                product_element.ProductId = parameter.ProductId;
                product_element.ProductName = parameter.ProductName;
                product_element.CategoryId = parameter.CategoryId;
                product_element.ManufactureId = parameter.ManufactureId;
                product_element.ProductPrice = parameter.ProductPrice;
                product_element.StockStatus = parameter.StockStatus;

                iproduct_repository.Update(product_element);

                ProductDetail product_detail_element = new ProductDetail();
                product_detail_element.ProductId = parameter.ProductId;
                product_detail_element.Microprocessor = parameter.Microprocessor;
                product_detail_element.Speed = parameter.Speed;
                product_detail_element.Graphics = parameter.Graphics;
                product_detail_element.RAM = parameter.RAM;
                product_detail_element.Capacity = parameter.Capacity;
                product_detail_element.Hardware = parameter.Hardware;
                product_detail_element.Monitor = parameter.Monitor;
                product_detail_element.Monitorsize = parameter.Monitorsize;
                product_detail_element.Operation = parameter.Operation;
                product_detail_element.Color = parameter.Color;
                product_detail_element.Connection = parameter.Connection;
                product_detail_element.Gate = parameter.Gate;
                product_detail_element.Webcam = parameter.Webcam;
                product_detail_element.Recognition = parameter.Recognition;
                product_detail_element.Battery = parameter.Battery;
                product_detail_element.Size = parameter.Size;
                product_detail_element.Weight = parameter.Weight;
                product_detail_element.Description = parameter.Description;
                product_detail_element.Core = parameter.Core;
                product_detail_element.Disc = parameter.Disc;

                iproduct_detail_repository.Update(product_detail_element);
                return Json(new { success = true, message = Properties.ProductResources.UPDATE_PRODUCT_SUCCESS }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = Properties.ProductResources.UPDATE_PRODUCT_FAILURE }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveImage(AlbumRequest request)
        {
            Album album = new Album();

            album.ProductId = request.ProductId;
            album.Alt = request.Alt;
            album.Title = request.Title;
            album.ImagePath = request.ImagePath;

            iproduct_image_repository.Insert(album);
            return Json(iproduct_image_repository.GetAllImages(request.ProductId), JsonRequestBehavior.AllowGet);
        }

        public string UploadFiles()
        {
            HttpPostedFileBase file = Request.Files[0];
            file = Request.Files[0];
            string filename = file.FileName;
            file.SaveAs(Server.MapPath("~/Content/Images/LaptopProduct/" + filename));
            return filename;
        }
    }
}
using ElectricStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly LaptopStoreEntities context = new LaptopStoreEntities();
        public ProductDetailController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        public ActionResult Index(int product_id)
        {
            return View();
        }

        // GET: ProductDetail
        public JsonResult ProductDetail(int product_id)
        {
            Product product_common = context.Products.Where(x => x.ProductId == product_id).First();
            ProductDetail product_detail = context.ProductDetails.Where(x => x.ProductId == product_id).First();

            ProductFullInfoResponse product_full_info_response = new ProductFullInfoResponse();

            // get information of product
            product_full_info_response.ProductId = product_common.ProductId;
            product_full_info_response.ProductName = product_common.ProductName;
            product_full_info_response.ProductPrice = product_common.ProductPrice;
            product_full_info_response.ProductImage = product_common.ProductImage;

            // get 
            string category_name = context.Categories.Where(x => x.CategoryId == product_common.CategoryId).First().CategoryName;
            product_full_info_response.CategoryName = category_name;

            // get manufacture name of product 
            string product_manufacture_name = context.Companies.Where(x => x.ManufactureId == product_common.ManufactureId).First().Manufacture;

            product_full_info_response.ManufactureName = product_manufacture_name;

            // get status of stock 
            Boolean status_stock = context.Stocks.Where(x => x.ProductId == product_common.ProductId).First().StockStatus;
            product_full_info_response.StockStatus = status_stock;

            // get detail of product
            product_full_info_response.Microprocessor = product_detail.Microprocessor;
            product_full_info_response.Graphics = product_detail.Graphics;
            product_full_info_response.RAM = product_detail.RAM;
            product_full_info_response.Hardware = product_detail.Hardware;
            product_full_info_response.Monitor = product_detail.Monitor;
            product_full_info_response.Operation = product_detail.Operation;
            product_full_info_response.Color = product_detail.Color;
            product_full_info_response.Connection = product_detail.Connection;
            product_full_info_response.Gate = product_detail.Gate;
            product_full_info_response.Webcam = product_detail.Webcam;
            product_full_info_response.Recognition = product_detail.Recognition;
            product_full_info_response.Battery = product_detail.Battery;
            product_full_info_response.Size = product_detail.Size;
            product_full_info_response.Weight = product_detail.Weight;
            product_full_info_response.Description = product_detail.Description;

            return Json(product_full_info_response, JsonRequestBehavior.AllowGet);
        }
    }
}
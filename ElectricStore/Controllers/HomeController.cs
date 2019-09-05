using Catel.MVVM.Providers;
using ElectricStore.Common;
using ElectricStore.Logic;
using ElectricStore.Logic.LogicExtension;
using ElectricStore.Models;
using ElectricStore.Models.Element;
using ElectricStore.Models.Request;
using ElectricStore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class HomeController : Controller
    {

        private readonly LaptopStoreEntities context = new LaptopStoreEntities();
        private readonly IPageLogic<Product> ipage_logic = new PageLogic<Product>();

        private readonly int PAGE_SIZE = 6;

        public HomeController(){
            context.Configuration.ProxyCreationEnabled = false;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSession()
        {
            if(Session[SessionData.SESSION_USER_NAME] is null)
            {
                return Json(ConstantStatus.LOG_OUT, JsonRequestBehavior.AllowGet);
            }
            string session_user_name = Session[SessionData.SESSION_USER_NAME].ToString();
            return Json(session_user_name, JsonRequestBehavior.AllowGet);
        }

        public void CloseSession()
        {
            Session.Clear();
        }

        public JsonResult GetCategory()
        {         
            List<Category> category_list = context.Categories.ToList();
            return Json(category_list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsByPagination(int current_page)
        {
            ProductListResponse response = new ProductListResponse();

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

        public List<Product> GetProduct()
        {
            List<Product> product_list = context.Products.ToList();
            return product_list;
        }

        //public JsonResult GetProduct()
        //{
        //    List<Product> product_list = context.Products.ToList();
        //    return Json(product_list, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult Login(UserRequest user_request)
        {
            string login_errors;
            if (ModelState.IsValid)
            {
                Account check_user = context.Accounts.Where(x => x.AccountName == user_request.Username && x.Password == user_request.Password).FirstOrDefault();
                if (!(check_user is null))
                {
                    Session[SessionData.SESSION_USER_NAME] = check_user.AccountName;
                    Session[SessionData.SESSION_USER_PASSWORD] = check_user.Password;

                    SessionInfo session_info = new SessionInfo();
                    session_info.Email = Session[SessionData.SESSION_USER_NAME].ToString();
                    session_info.Password = Session[SessionData.SESSION_USER_PASSWORD].ToString();
                    return Json(session_info, JsonRequestBehavior.AllowGet);
                }
                return Json("Tài khoản này không chính xác");
            }
            login_errors = string.Join("\n", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)).ToString();
            return Json(login_errors);
        }

        [HttpPost]
        public JsonResult Register(RegisterRequest register_request)
        {
            string login_errors;
            if (ModelState.IsValid)
            {
                Account account = new Account();
                account.AccountName = register_request.Email;
                account.Password = register_request.Password;
                account.Address = register_request.Address;
                // Register from Front page is always guest
                account.RoleId = RoleValue.GUEST;
                context.Accounts.Add(account);
                context.SaveChanges();
                return Json(account, JsonRequestBehavior.AllowGet);
            }
            login_errors = string.Join("\n", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)).ToString();
            return Json(login_errors);
        }

        
        public JsonResult Cart(int product_id)
        {
            Product cart_product = context.Products.Where(x => x.ProductId == product_id).First();
            if(cart_product is null)
            {
                return Json("Sản phẩm này không tồn tại", JsonRequestBehavior.AllowGet);
            }

            if(Session[SessionData.SESSION_CART] is null)
            {
                List<CartItem> cart_item_list = new List<CartItem>();
                // assign value for cart
                CartItem cart_item = new CartItem();
                cart_item.ProductName = cart_product.ProductName;
                cart_item.ProductPrice = cart_product.ProductPrice;
                cart_item.Quantity = CartValue.original_quantity;
                cart_item.Image = cart_product.ProductImage;

                // add item into cart
                cart_item_list.Add(cart_item);

                Session[SessionData.SESSION_CART] = cart_item_list;
                return Json(cart_item_list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<CartItem> cart_item_list = (List<CartItem>)Session[SessionData.SESSION_CART];
                // Check the item added to cart is existing in cart or not ?
                CartItem cart_item = new CartItem();
                cart_item.ProductName = cart_product.ProductName;
                cart_item.Quantity = CartValue.original_quantity;
                cart_item.ProductPrice = cart_product.ProductPrice;
                cart_item.Image = cart_product.ProductImage;

                Boolean check_flg = cart_item_list.Any(x => x.ProductName == cart_item.ProductName);                

                if (check_flg)
                {
                    int now_quantity = cart_item_list.Where(x => x.ProductName == cart_item.ProductName).First().Quantity;
                    // Increase the quantity of cart item
                    cart_item.Quantity = now_quantity + CartValue.increase_quantity;
                    int index = cart_item_list.FindIndex(x => x.ProductName == cart_item.ProductName);
                    cart_item_list[index].Quantity = cart_item.Quantity; 
                }
                else
                {
                    cart_item_list.Add(cart_item);
                }
                Session[SessionData.SESSION_CART] = cart_item_list;
                return Json(cart_item_list, JsonRequestBehavior.AllowGet);
            }         
        }

        public JsonResult DisplayQuantityCart()
        {
            if (Session[SessionData.SESSION_CART] is null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            List<CartItem> cart_item_list = (List<CartItem>)Session[SessionData.SESSION_CART];
            int items_quantity = 0;
            foreach (var cart_item in cart_item_list)
            {
                items_quantity += cart_item.Quantity;
            }

            return Json(items_quantity, JsonRequestBehavior.AllowGet);
        }       
    }
}
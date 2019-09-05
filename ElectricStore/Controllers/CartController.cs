using ElectricStore.Common;
using ElectricStore.Models.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {           
            return View();
        }

        public JsonResult DisplayCart()
        {
            List<CartItem> cart_item_list = (List<CartItem>)Session[SessionData.SESSION_CART];
            return Json(cart_item_list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveCartItem(string product_name)
        {
            List<CartItem> cart_item_list = (List<CartItem>)Session[SessionData.SESSION_CART];
            CartItem remove_item = cart_item_list.Find(x => x.ProductName == product_name);
            cart_item_list.Remove(remove_item);
            return DisplayCart();
        }
    }
}
using ElectricStore.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectricStore.Controllers
{
    public class LoginAdminController : Controller
    {
        LaptopStoreEntities context = new LaptopStoreEntities();
        // GET: LoginAdmin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAdminRequest request)
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class LoginAdminRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
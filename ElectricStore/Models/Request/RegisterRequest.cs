using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectricStore.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [StringLength(30, ErrorMessage = "Tài khoản bạn nhập vượt quá số kí tự cho phép")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [StringLength(14, ErrorMessage = "Mật khẩu không đúng")]
        public string Password { get; set; }        
        public string Address { get; set; }
        //public string Image { get; set; }
    }
}
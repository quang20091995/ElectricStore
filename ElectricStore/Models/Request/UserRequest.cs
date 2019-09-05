using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectricStore.Models
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [StringLength(30, ErrorMessage = "Tài khoản bạn nhập vượt quá số kí tự cho phép")]
        public String Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [StringLength(14, ErrorMessage = "Mật khẩu không đúng")]
        public String Password { get; set; }
    }
}
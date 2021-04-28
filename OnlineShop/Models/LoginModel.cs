using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập tên tài khoản")]
        [DisplayName("Tên Tài Khoản")]
        public string UserName { get; set; }

        [StringLength(32, MinimumLength = 6, ErrorMessage = "Độ dài password ít nhất phải có 6 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [DisplayName("Mật Khẩu")]
        public string Password { get; set; }
    }
}
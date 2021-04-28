using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập tên tài khoản")]
        [DisplayName("Tên Tài Khoản")]
        public string UserName { get; set; }

        [StringLength(32,MinimumLength =6, ErrorMessage ="Độ dài password ít nhất phải có 6 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        [DisplayName("Mật Khẩu")]
        public string Password { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Yêu cầu xác nhận mật khẩu")]
        [DisplayName("Xác Nhận Mật Khẩu")]
        [Compare("Password",ErrorMessage ="Xác Nhận Mật Khẩu Không Đúng")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        [DisplayName("Tên")]
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Yêu cầu nhập tên email")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

        [DisplayName("Tỉnh/Thành")]
        public int ProvinceID { get; set; }

        [DisplayName("Quận/Huyện")]
        public int DistrictID { get; set; }

        [DisplayName("Xã/Phường")]
        public int TownID { get; set; }
    }
}
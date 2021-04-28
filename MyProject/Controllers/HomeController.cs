using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using Models.DAO;
using Models.Framework;
using MyProject.Common;
using PagedList.Mvc;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
       UserDao dao = new UserDao();
        OnlineShopDBContext db = new OnlineShopDBContext();
        public ActionResult Index()
        {
         
            return View();
           
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            //return PartialView(db.Menus.Where(x => x.TypeID == 1 && x.Status == true).OrderBy(x => x.DisplayOrder).ToList());
            return PartialView(db.Footers.SqlQuery("select * from Footer where Status=1").SingleOrDefault());
        }
        [HttpGet]//Get là tải trang giao diện
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]//Post là post lên
        [CaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel register, string CaptchaCode)
        {
            if (ModelState.IsValid)
            {
                List<object> lst = new List<object>();
                if (dao.checkUserName(register.UserName))
                {

                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại rồi");
                }
                else
                {
                    lst.Add(register.UserName);

                }
                lst.Add(Encryptor.EncMD5(register.Password));
                lst.Add(register.Name);
                lst.Add(register.Address);
                if (dao.checkEmail(register.Email))
                {

                    ModelState.AddModelError("", "Email đã tồn tại rồi");
                }
                else
                {
                    lst.Add(register.Email);

                }
                if (dao.checkPhone(register.Phone))
                {

                    ModelState.AddModelError("", "Số điện thoại đã tồn tại rồi");
                }
                else
                {
                    lst.Add(register.Phone);
                }
                lst.Add(register.ProvinceID);
                lst.Add(register.DistrictID);
                object[] ls = lst.ToArray();
                if (ls.Length == 8)
                {
                   
                    
                        var result = db.Database.ExecuteSqlCommand("insert into [User](UserName,Password,Name,Address,Email,Phone,ProvinceID,DistrictID) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", ls);
                        if (result > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Đăng ký không thành công.");
                        }
                    
                  
                }

            }
            
            return View(register);
        }
    }
}
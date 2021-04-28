using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using OnlineShop.Common;
using PagedList.Mvc;
namespace OnlineShop.Controllers
{
    public class AboutController : Controller
    {
        UserDao dao = new UserDao();
        OnlineShopDBContext db = new OnlineShopDBContext();
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]//Get là tải trang giao diện
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]//Post là post lên
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                List<object> lst = new List<object>();
                if (dao.checkUserName(user.UserName))
                {

                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại rồi");
                }
                else
                {
                    lst.Add(user.UserName);

                }
                lst.Add(Encryptor.EncMD5(user.Password));
                lst.Add(user.Name);
                lst.Add(user.Address);
                if (dao.checkEmail(user.Email))
                {

                    ModelState.AddModelError("", "Email đã tồn tại rồi");
                }
                else
                {
                    lst.Add(user.Email);

                }
                if (dao.checkPhone(user.Phone))
                {

                    ModelState.AddModelError("", "Số điện thoại đã tồn tại rồi");
                }
                else
                {
                    lst.Add(user.Phone);
                }
                lst.Add(user.ProvinceID);
                lst.Add(user.DistrictID);
                object[] ls = lst.ToArray();
                if (ls.Length == 8)
                {
                    var result = db.Database.ExecuteSqlCommand("insert into [User](UserName,Password,Name,Address,Email,Phone,ProvinceID,DistrictID) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", ls);
                    if (result > 0)
                    {
                        return RedirectToAction("Create", "Categoryy");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Tạo tài khoản không thành công");

            }
            return View(user);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using OnlineShop.Common;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        public ActionResult Index()
        {
            var result = db.Users.SqlQuery("select * from [User]").ToList();
            return View(result);
        }
        // GET: Admin/User
        [HttpGet]//Get là tải trang giao diện
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]//Post là post lên
        public ActionResult Create(User user)
        {
            var dao = new UserDao();
           user.Password= Encryptor.EncMD5(user.Password);
            long id = dao.Insert(user);
            if (id > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tạo tài khoản không thành công");
                return View();
            }
        }
    }
}
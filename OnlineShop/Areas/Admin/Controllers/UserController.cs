using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using OnlineShop.Common;
using PagedList.Mvc;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserDao dao = new UserDao();
      OnlineShopDBContext db = new OnlineShopDBContext();
        public ActionResult Index(string searchString, int page=1, int pagesize=2)
        {
            var result = dao.ListAll(searchString, page,pagesize);
            ViewBag.searchString = searchString;
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
            dao = new UserDao();
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
        [HttpGet]
        public ActionResult Update(int id)
        {           
                return View(dao.GetUserID(id));       
           
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        user.Password = Encryptor.EncMD5(user.Password);
                    }
                   
                    if (dao.Update(user))
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }         
                           
              
            }
            return View();
           
        }
     
        public ActionResult Delete(int id)
        {
           
            if (dao.Delete(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
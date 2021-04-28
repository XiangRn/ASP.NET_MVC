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
    public class UserController : BaseController
    {
        UserDao dao = new UserDao();
      OnlineShopDBContext db = new OnlineShopDBContext();
        public ActionResult Index(string searchString, int page =1, int pagesize=2)
        {
            var result = dao.ListAll(searchString, page,pagesize);
            ViewBag.searchString = searchString;
            return View(result);
        }
        public ActionResult LUser(int page = 1, int pagesize = 2)
        {
            var result = dao.ListEx( page, pagesize);
    
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
                        return RedirectToAction("Create","Categoryy","Admin");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Tạo tài khoản không thành công");

            }
            return View(user);
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
                        SetAlert("Update a new User successfully", "success");
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
        [HttpPost]      
        public JsonResult ChangeStatus(long id)
        {
            var result = dao.ChangeStatus(id);
            return Json(new
            {                
                status = result
            });
        }
    }
}
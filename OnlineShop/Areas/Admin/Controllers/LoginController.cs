using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Areas.Admin.Code;
using System.Web.Security;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        OnlineShopDBContent db = new OnlineShopDBContent();
        // GET: Admin/Login
        [HttpGet]//httpGet có thể gọi nhận từ URL
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Ở trên Server sẽ sinh ra một cái token, ở dưới client có một cái token tương tự
        //Hai cái nó khớp với nhau đảm bảo request và respone đồng bộ
        //Để chống cái việc request liên tục
        public ActionResult Index(Account account)
        {
            //var res= db.Accounts.Where(x=>x.UserName==account.UserName && x.PassWord==account.PassWord).Count();
            //var result = new AccountModel().Login(model.UserName, model.Password);
            //if (result != null && ModelState.IsValid)
            //{
            //    SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            //}
            //if (ModelState.IsValid)
            //{
                //try
                //{
                //    if (res > 0)
                //    {
                //SessionHelper.SetSession(new UserSession() { UserName =account.UserName});
                //return RedirectToAction("Index", "Home");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                //        return View();
                //    }
                //}
                //catch (Exception ex)
                //{

                //    ModelState.AddModelError("", ex.Message);
                //}
                
            //}
            //return View(account);
           if(Membership.ValidateUser(account.UserName, account.PassWord) && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(account.UserName, account.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//hủy tất cả nhưng cookie
            return RedirectToAction("Index", "Login");
        }
       
    }
}
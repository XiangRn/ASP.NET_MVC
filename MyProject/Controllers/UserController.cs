using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using Models.Framework;
using Models.DAO;
using MyProject.Common;
using System.Configuration;
using Facebook;
using MyProject.Models;
using BotDetect.Web.Mvc;
namespace MyProject.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        OnlineShopDBContext db = new OnlineShopDBContext();
        UserDao dao = new UserDao();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (dao.checkUserName(username))
                {
                    if (dao.checkAccount(username, Encryptor.EncMD5(password)))
                    {
                        if (dao.checkAccountIsActive(username))
                        {

                            return Json(new
                            {
                                error = "Tài khoản này bị vô hiệu hóa, Bạn cần liên hệ với shop để trao đổi thêm !!!"

                            });
                        }
                        else
                        {
                            Session["username"] = username;
                            return Json(new
                            {
                                finish = "success"

                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            error = "Mật khẩu không đúng !!!"

                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        error = "Tên tài khoản hoặc mật khẩu không đúng !!!"

                    });
                }
            }
            else
            {
                return Json(new
                {
                    error = "Đăng nhập thất bại!!!"

                });
            }
        }

        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                //string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                if (dao.checkEmail(email))
                {
                    Session["username"] = db.Users.SingleOrDefault(x => x.Email == email).UserName;
                }
                else
                {
                    List<object> lst = new List<object>();
                    lst.Add(lastname + " " + middlename + " " + firstname);
                    lst.Add(email);
                    object[] ls = lst.ToArray();
                    var rst = db.Database.ExecuteSqlCommand("insert into [User](Name, Email) values(@p0,@p1)", ls);
                    if (rst > 0)
                    {
                        Session["username"] = email;
                    }
                }

                //var user = new User();
                //user.Email = email;
                //user.UserName = email;
                //user.Status = true;
                //user.Name = firstname + " " + middlename + " " + lastname;
                //user.CreatedDate = DateTime.Now;
                //var resultInsert = new UserDao().InsertForFacebook(user);
                //if (resultInsert > 0)
                //{
                //    var userSession = new UserLogin();
                //    userSession.UserName = user.UserName;
                //    userSession.UserID = user.ID;
                //    Session.Add(CommonConstants.USER_SESSION, userSession);
                //}
            }
            return Redirect("/trang-chu");
        }
        public JsonResult Logout()
        {
            Session["username"] = null;
            return Json(new
            {
                status = true
            });
        }
    }
}
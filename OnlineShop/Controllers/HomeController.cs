using Models.DAO;
using System.Collections.Generic;
using Models.Framework;
using System.Linq;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using OnlineShop.Common;
using OnlineShop.Models;
using System.Configuration;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        UserDao dao = new UserDao();
        public ActionResult Index()
        {
            ProductDAO dao = new ProductDAO();
            ViewBag.NewProduct = dao.NewsArrivals(6);
            ViewBag.BestProduct = dao.LastedProducts(4);
            //set Tittle
            ViewBag.Tittle= ConfigurationManager.AppSettings["Tittle"].ToString();
            ViewBag.keywords = ConfigurationManager.AppSettings["keywords"].ToString();
            ViewBag.description = ConfigurationManager.AppSettings["description"].ToString();
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600*24)]
        public ActionResult MainMenu()
        {
            //return PartialView(db.Menus.Where(x => x.TypeID == 1 && x.Status == true).OrderBy(x => x.DisplayOrder).ToList());
            //ViewBag.ProductList = db.ProductCategories.SqlQuery("select * from ProductCategory order by DisplayOrder").ToList();
            return PartialView(db.Menus.SqlQuery("select * from Menu where TypeID=1 and Status =1 order by DisplayOrder").ToList());
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            //return PartialView(db.Menus.Where(x => x.TypeID == 1 && x.Status == true).OrderBy(x => x.DisplayOrder).ToList());
            return PartialView(db.Footers.SqlQuery("select * from Footer where Status=1").SingleOrDefault());
        }
        [ChildActionOnly]//chỉ dành partialview được gọi thôi
        public PartialViewResult _HomeProductListMenu()
        {
            //return PartialView(db.Menus.Where(x => x.TypeID == 1 && x.Status == true).OrderBy(x => x.DisplayOrder).ToList());
            return PartialView(db.ProductCategories.SqlQuery("select * from ProductCategory order by DisplayOrder").ToList());
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
                lst.Add(register.TownID);
                object[] ls = lst.ToArray();
                if (ls.Length >= 8)
                {


                    var result = db.Database.ExecuteSqlCommand("insert into [User](UserName,Password,Name,Address,Email,Phone,ProvinceID,DistrictID,TownID) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", ls);
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
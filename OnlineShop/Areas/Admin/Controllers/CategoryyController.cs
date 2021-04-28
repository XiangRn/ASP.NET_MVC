using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryyController : BaseController
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        // GET: Admin/Categoryy
        public ActionResult Index()
        {
            return View();
        }
     [HttpGet]
        public ActionResult Create()
        {
      
            return View();
        }
      [HttpPost]
      public ActionResult Create(Categoryy categoryy)
        {
            if (ModelState.IsValid)
            {
                List<object> ls = new List<object>();
                ls.Add(categoryy.Name);
                ls.Add(categoryy.MetaTitle);
                ls.Add(categoryy.ParentID);
                ls.Add(categoryy.DisplayOrder);
                ls.Add(categoryy.SeoTitle);
                ls.Add(categoryy.MetaKeywords);
                ls.Add(categoryy.Status);
                ls.Add(categoryy.ShowOnHome);
                object[] lst = ls.ToArray();
                var result = db.Database.ExecuteSqlCommand("insert into Categoryy(Name,MetaTitle,ParentID,DisplayOrder,SeoTitle,MetaKeywords,Status,ShowOnHome) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)", lst);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tạo không thành công");
            }
            return View(categoryy);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models.DAO;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        ContentDAO dao = new ContentDAO();
        // GET: Admin/Content
        public ActionResult Index()
        {
            return View(dao.ListAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dao.Create(content))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create failure");
                        return View();
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
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(dao.GetById(id));
        }
        [HttpPost]
        [ValidateInput(false)]//để nó ko bị lỗi khi tạo dữ liệu cho ckeditor và ckfinder
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dao.Edit(content))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failure");
                        return View();
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
namespace OnlineShop.Controllers
{
    public class ContentController : Controller
    {

        ContentDAO dao = new ContentDAO();
        // GET: Content
        public ActionResult LContent(int page = 1, int pagesize = 2)
        {
            var result = dao.ListEx(page, pagesize);

            return View(result);
        }
        public ActionResult Details(long id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ListTags = dao.ListTags(id);
                return View(dao.getID(id));
            }
            return View();
        }
        public ActionResult DetailTag(string TagID, int page=1, int pagesize=2)
        {
            ViewBag.tagID = TagID;
            return View(dao.DetailTags(TagID,page,pagesize));
        }
    }
}
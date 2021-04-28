using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Common;
using System.Web.Routing;
using Models.Framework;
namespace MyProject.Areas.Admin.Controllers
{
    //[Authorize]//tự động check login
 
    public class HomeController : Controller
    {
        // GET: Admin/Home
     
        public ActionResult Index()
        {
            var session = Session["UserID"];
            if (session != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
       
        }
       
    }
}
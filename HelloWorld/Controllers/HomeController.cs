using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Message = "Chào mừng Viên Viên đến với nhóm chúng tôi";
            MessageModel message = new MessageModel();
            message.Welcome = "Viên Viên";
            return View(message);
        }
    }
}
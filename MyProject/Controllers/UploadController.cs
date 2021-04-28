using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Upload upload)
        {
            if (!string.IsNullOrEmpty(upload.Image) && !string.IsNullOrEmpty(upload.Comment))
            {
                ViewBag.image = upload.Image;
                ViewBag.com = upload.Comment;
                return View();
            }
            return View();
        }
    }
}
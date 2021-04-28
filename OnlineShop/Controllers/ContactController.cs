using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Common;
namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address,string email, string content)
        {
            if(new ContactDAO().SendFeedback(name, mobile, address, email, content))
            {
                string cont = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/feedback.html"));

                cont = cont.Replace("{{CustomerName}}", name);
                cont = cont.Replace("{{Phone}}", mobile);
                cont = cont.Replace("{{Email}}", email);
                cont = cont.Replace("{{Address}}", address);
                cont = cont.Replace("{{Content}}", content);


                new MailHelper().SendMailToAdmin(email, "FeedBack Từ Khách Hàng", cont);
                return Json(new
                {
                    status = true,


                });
           
            }
            return Json(new
            {
                status = false
            }) ;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Common;
using System.Web.Routing;
using Models.Framework;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public ActionResult Index()
        {
            return View();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            HttpSessionStateBase ses = filterContext.HttpContext.Session;
            var session = Session[CommonConstants.USER_SESSION];
            if (ses != null && session == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller="Login", action="Index",area="Admin"}));
                filterContext.ExceptionHandled = true;
            }
            base.OnException(filterContext);
        }
    }
}
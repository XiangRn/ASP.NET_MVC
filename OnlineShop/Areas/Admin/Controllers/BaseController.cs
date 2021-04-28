using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using OnlineShop.Common;
using System.Web.Routing;
using Models.Framework;
using System.Globalization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CurrentCulture"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CurrentCulture"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CurrentCulture"].ToString());
            }
            else
            {
                Session["CurrentCulture"] = "vi";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi");
            }
        }
        // changing culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);

            Session["CurrentCulture"] = ddlCulture;
            return Redirect(returnUrl);
        }
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    HttpSessionStateBase ses = filterContext.HttpContext.Session;
        //    var session = Session[CommonConstants.USER_SESSION];
        //    if (ses != null && session == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //            new RouteValueDictionary(new { controller="Login", action="Index",area="Admin"}));
        //        filterContext.ExceptionHandled = true;
        //    }
        //    base.OnException(filterContext);
        //}
        protected void SetAlert(string message, string type)
        {
            TempData["MessageData"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if(type=="error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
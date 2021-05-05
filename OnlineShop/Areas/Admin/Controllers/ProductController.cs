using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(db.Productts.SqlQuery("select * from Productt "));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Productt productt)
        {
            if (ModelState.IsValid)
            {
                
                db.Productts.Add(productt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public JsonResult LoadImages(long id)
        {
            ProductDAO dao = new ProductDAO();
            var product = dao.GetID(id);
            var image = product.MoreImages;
            XElement xElement = XElement.Parse(image);
            List<string> listImages = new List<string>();
            foreach (var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }
            return Json(new
            {
                data = listImages,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveImages(long id,string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);//truyền vào 1 list string
            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {
                xElement.Add(new XElement("Image", item.Substring(item.IndexOf("D") - 1)));
            }
            ProductDAO dao = new ProductDAO();
            dao.UpdateImages(id, xElement.ToString());
            return Json(new
            {
                status = true
            });
        }
    
        public ActionResult Details(long id)
        {
            ProductDAO dao = new ProductDAO();
            var product = dao.GetID(id);
            var image = product.MoreImages;
            XElement xElement = XElement.Parse(image);
            List<string> listImages = new List<string>();
            foreach (var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }
            ViewBag.listImages = listImages;
            return View(product);
        }
        public JsonResult SaveImg(long id, string images)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var listImages = js.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {
                xElement.Add(new XElement("Image",item.Substring(item.IndexOf("D")-1)));
            }
            ProductDAO dao = new ProductDAO();
            dao.UpdateImages(id, xElement.ToString());
            return Json(new
            {
                status = true
            }) ;
        }
        public JsonResult Loading(long id)
        {
            ProductDAO dao = new ProductDAO();
           
            XElement xElement = XElement.Parse(dao.GetID(id).MoreImages);
            List<string> listImages = new List<string>();
            foreach (var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }
            return Json(new
            {
                data = listImages,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
using Models.DAO;
using PagedList;
using System.Xml.Linq;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO dao = new ProductDAO();
        OnlineShopDBContext db = new OnlineShopDBContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(long id, int page=1, int pagesize=2)
        {
            ViewBag.ProductCategory =db.ProductCategories.Find(id) ;
            var result = dao.ListEx(id,page,pagesize);
            return View(result);
        }
        public ActionResult Detail(long id)
        {            
            var product = dao.GetID(id);
            var image = product.MoreImages;
            XElement xElement = XElement.Parse(image);
            List<string> listImages = new List<string>();
            foreach (var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }
            ViewBag.listImages = listImages;
            ViewBag.Category = db.ProductCategories.SqlQuery("select * from ProductCategory where ID=@p0", db.Productts.Find(id).CategoryID).SingleOrDefault();
            return View(product);
        }
        public ActionResult ListName(string q)
        {
            var data = dao.ListName(q);
            return Json(new
            {
                data=data,
                status=true
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1, int pagesize = 2)
        {
          
                ViewBag.keyword = keyword;
                var result = dao.Search(keyword, page, pagesize);
                if (result.Count() > 0)
                {                    
                    return View(result);
                }
                else
                {
                    ViewBag.Message = "Không có sản phẩm nào";
                    return View(result);
                }
               
           
            
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using Models.Framework;
using Models.DAO;
using System.Configuration;
using System.Web.Script.Serialization;
using Common;
using Rotativa;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        private const string CartSession = "CartSession";//const có nghĩa là đây là hằng số không thể thay đổi được
        // GET: Cart
        [HttpGet]
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(string shipName, string mobile, string address, string email)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            List<object> ls = new List<object>();
            ls.Add(shipName);
            ls.Add(mobile);
            ls.Add(address);
            ls.Add(email);
            object[] lst = ls.ToArray();
           
            var result = db.Database.ExecuteSqlCommand("insert into [Order](CreatedDate,ShipName,ShipMobile,ShipAddress,ShipEmail) values(getdate(),@p0,@p1,@p2,@p3)", lst);
            if (result > 0)
            {
                var order = db.Orders.SqlQuery("select top 1 * from [Order] order by ID desc").SingleOrDefault();               
                decimal total = 0;
                if (order !=null)
                {
                    double sum = 0;
                    List<object> ls1 = null; string s = string.Empty;
                    s += "Cảm ơn quý khách đã đặt hàng tại cửa hàng của Toys-Shop của chúng tôi \n";
                    s += "Người mua hàng : "+shipName+"\n";
                    
                    if (sessionCart != null){
                        foreach (var item in sessionCart)
                        {
                           ls1 = new List<object>();
                           s +="Sản Phẩm : "+item.Product.Name +"\n Số Lượng : "+item.Quantity+"\n Gía:"+item.Product.Price+"\n" ;
                            sum += Convert.ToDouble((item.Quantity * item.Product.Price));
                            ls1.Add(item.Product.ID);
                            ls1.Add(order.ID);
                            ls1.Add(item.Quantity);
                            ls1.Add(item.Product.Price);
                            object[] lst1 = ls1.ToArray();
                            result = db.Database.ExecuteSqlCommand("insert into [OrderDetail](ProductID,OrderID,Quantity,Price) values(@p0,@p1,@p2,@p3)", lst1);

                            total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                        }
                        s += "Tổng Tiền : "+sum.ToString();
                        Session["Bill"] = s;
                    }
                  
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/neworder.html"));
                
                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
             
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
               
                return RedirectToAction("GeneratePDF", "Sample");
            }
            return View();
           
        }
        public ActionResult AddItem(long productid, int quantity)
        {
            ProductDAO dao = new ProductDAO();
            Productt product = dao.GetID(productid);
            List<CartItem> lst = new List<CartItem>();
            var cart = Session[CartSession];
            if (cart != null)
            {
                lst = (List<CartItem>)cart;
                if (lst.Where(x => x.Product.ID == productid).SingleOrDefault()!= null)
                {
                    //foreach (var item in lst)
                    //{
                    //    if (item.Product.ID == productid)
                    //    {
                    lst.Where(x => x.Product.ID == productid).SingleOrDefault().Quantity += quantity;
                    //    }

                    //}
                    
                }
                else
                {
                    var c = new CartItem();
                    c.Product = product;
                    c.Quantity = quantity;
                    lst.Add(c);
                  
                }
                Session[CartSession] = lst;

            }
            else
            {
                var c = new CartItem();
                c.Product = product;
                c.Quantity = quantity;
                lst.Add(c);
                Session[CartSession] = lst;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult DeleteEachItem(int productId)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == productId);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x=>x.Product.ID==item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            }) ;
        }
    }
}
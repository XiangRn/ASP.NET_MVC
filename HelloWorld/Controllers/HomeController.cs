using HelloWorld.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using System.Web.Script.Serialization;
namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
        public static string sessioncart = "SessionCart";
        public ActionResult Index()
        {
            ViewBag.Message = "Chào mừng Viên Viên đến với nhóm chúng tôi";
            MessageModel message = new MessageModel();
            message.Welcome = "Viên Viên";
            return View(message);
        }
        
        public ActionResult ProductList()
        {
            return View(db.Productts);
        }
        public JsonResult AddCart(long id, int quantity=1)
        {
            ProductDAO dao = new ProductDAO();
            var product = dao.GetID(id);
            List<CartItem> lst = new List<CartItem>();
            var cart = Session[sessioncart];
            if (cart != null)
            {
                lst = (List<CartItem>)Session[sessioncart];
                if (lst.Where(x => x.Product.ID == id).SingleOrDefault() != null)
                {
                    lst.Where(x => x.Product.ID == id).SingleOrDefault().Quantity += quantity;
                }
                else
                {
                    CartItem cartItem = new CartItem();
                    cartItem.Product = product;
                    cartItem.Quantity = 1;
                    lst.Add(cartItem);
                    
                }
                Session[sessioncart] = lst;
            }
            else
            {
                CartItem cartItem = new CartItem();
                cartItem.Product = product;
                cartItem.Quantity = 1;
                lst.Add(cartItem);
                Session[sessioncart] = lst;
            }
            return Json(new
            {
                status=true

            });
        }
        public ActionResult Cart()
        {
            var cart = Session[sessioncart];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);

        }
        public JsonResult UpdateItem(long id, int quantity)
        {
            ProductDAO dao = new ProductDAO();
            var product = dao.GetID(id);
            var list = (List<CartItem>)Session[sessioncart];
            if (list.Where(x => x.Product.ID == id).SingleOrDefault() != null)
            {
                list.Where(x => x.Product.ID == id).SingleOrDefault().Quantity = quantity;
            }
            Session[sessioncart] = list;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string quantity)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var listCart = js.Deserialize<List<CartItem>>(quantity);
            var cart = (List<CartItem>)Session[sessioncart];
            foreach (var item in listCart)
            {
                if (cart.Where(x => x.Product.ID == item.Product.ID).SingleOrDefault() != null)
                {
                    cart.Where(x => x.Product.ID == item.Product.ID).SingleOrDefault().Quantity = item.Quantity;
                }
            }
            return Json(new
            {

                status=true
            });
        }
        public JsonResult DeleteItem(long id)
        {
            var cart = (List<CartItem>)Session[sessioncart];
            cart.RemoveAll(x => x.Product.ID == id);
            Session[sessioncart] = cart;
            return Json(new
            {
                status=true
            });
        }
        public JsonResult Delete()
        {
            Session[sessioncart] = null;
            return Json(new
            {
                status=true
            });
        }
        public ActionResult GetAllUser(int page=1, int pageSize=3)
        {
            UserDao dao = new UserDao();
            return View(dao.ListEx(page,pageSize));
        }
        public JsonResult ChangeStatus(long id)
        {
           ChangStatus changeStatus = new ChangStatus();
            bool result = changeStatus.Change(id);
            return Json(new
            {
                status = result

            }) ;
        }
    }
}
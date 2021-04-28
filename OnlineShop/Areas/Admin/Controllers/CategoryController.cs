using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Framework;
namespace MyProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
       OnlineShopDBContext db = new OnlineShopDBContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
           var ls= db.Categories.SqlQuery("select * from Category order by [Order] asc").ToList();
            return View(ls);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]//khi bấm nút Submit nó sẽ trả về cái method HTTPPOST
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<object> lst = new List<object>();
                    lst.Add(category.Name);
                    lst.Add(category.Alias);
                    lst.Add(category.ParentID);
                    lst.Add(category.Order);
                    lst.Add(category.Status);
                   object[] ls = lst.ToArray();
                    int output = db.Database.ExecuteSqlCommand("exec usp_Category_insert @name = @p0, @alias = @p1,  @parentid=@p2, @status = @p4", ls);
                    if (output > 0)
                    {
                       // db.SaveChanges();
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Chưa tạo thành công");
                        return View();
                    }
                }
                else
                {
                    return View();
                }
                

              
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

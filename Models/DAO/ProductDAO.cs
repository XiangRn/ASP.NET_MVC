using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using PagedList;
namespace Models.DAO
{
    public class ProductDAO
    {
        OnlineShopDBContext db = null;
        public ProductDAO()
        {
            db = new OnlineShopDBContext();
        }
        public IEnumerable<Productt> NewsArrivals(int top)
        {
            return db.Productts.OrderBy(x => x.CreatedDate).Take(top).ToList();
        }
        public IEnumerable<Productt> LastedProducts(int top)
        {
            return db.Productts.Where(x => x.TopHot != null && x.TopHot >= DateTime.Now && x.ID >= 6).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public IEnumerable<Productt> ListRelatedProducts(int productId)
        {
            return db.Productts.Where(x => x.ID != productId && x.CategoryID==(db.Productts.Find(productId).CategoryID)).ToList();
        }
        public IEnumerable<Productt> ListEx(long id,int page, int pagesize)
        {
            return db.Productts.SqlQuery("select * from Productt where CategoryID=" + id).ToPagedList(page, pagesize);
        }
        public Productt GetID(long id)
        {
            return db.Productts.Find(id);
        }
        public List<string> ListName(string keyword)
        {
            return db.Productts.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public IEnumerable<Productt> Search(string keyword, int page, int pagesize)
        {
            return db.Productts.SqlQuery("select * From Productt where Name like '%" + keyword + "%'").ToPagedList(page, pagesize);
        }
    }
}

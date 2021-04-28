using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
namespace Models.DAO
{
  public  class CategoryyDao
    {
        OnlineShopDBContext db = null;
        public CategoryyDao()
        {
            db = new OnlineShopDBContext();
        }
        public IEnumerable<Categoryy> ListAll()
        {
            return db.Categoryies.SqlQuery("select * from Categoryy where Status=1").ToList();
        }
      
    }
}

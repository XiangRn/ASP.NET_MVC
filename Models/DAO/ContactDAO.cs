using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
namespace Models.DAO
{
    public class ContactDAO
    {
        OnlineShopDBContext db = null;
        public ContactDAO()
        {
            db = new OnlineShopDBContext();
        }
        public bool SendFeedback(string name, string mobile, string address, string email, string content)
        {
            List<object> ls = new List<object>();
            ls.Add(name);
            ls.Add(mobile);
            ls.Add(email);
            ls.Add(address);           
            ls.Add(content);
            object[] lst = ls.ToArray();
            var result = db.Database.ExecuteSqlCommand("insert into Feedback(Name,Phone,Email,Address,Content,CreatedDate) values(@p0,@p1,@p2,@p3,@p4,getdate())", lst);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

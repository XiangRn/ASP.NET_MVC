using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models.DAO
{
    public class UserDao
    {
        OnlineShopDBContext db = null;
        public UserDao()
        {
            db = new OnlineShopDBContext();
        }
        public long Insert(User user)
        {
            List<object> lst = new List<object>();
            lst.Add(user.UserName);
            lst.Add(user.Password);
            lst.Add(user.Name);
            lst.Add(user.Address);
            lst.Add(user.Email);
            lst.Add(user.Phone);
            lst.Add(user.Status);
            object[] ls = lst.ToArray();
            var result = db.Database.ExecuteSqlCommand("insert into [User](UserName,Password,Name,Address,Email,Phone,Status) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", ls);
            if (result >0)
            {
                return result;
            }
            else
            {
                return 0;
            }
           
        }
        public int Login(string username, string password)
        {
            
            var result = db.Users.SingleOrDefault(x=>x.UserName==username);
            //var result1 = db.Users.Count(x => x.UserName == username && x.Password == password);cách thứ 2 dùng LINQ
            if (result == null)
            {

                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }

                }
            }
          
        }
    }
}

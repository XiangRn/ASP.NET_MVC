using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using PagedList;

namespace Models.DAO
{
    public class UserDao
    {
        OnlineShopDBContext db = null;
        public UserDao()
        {
            db = new OnlineShopDBContext();
        }
        public User GetUserID(int id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User user)
        {
            List<object> lst = new List<object>();
            lst.Add(user.UserName);
            lst.Add(user.Password);
            lst.Add(user.Name);
            lst.Add(user.Address);
            lst.Add(user.Email);
            lst.Add(user.Phone);
            lst.Add(user.ModifiedBy);
            lst.Add(user.ModifiedDate);
            lst.Add(user.Status);
            lst.Add(user.ID);
            object[] ls = lst.ToArray();
            var result = db.Database.ExecuteSqlCommand("declare @date datetime=getdate();update [User] set UserName=@p0,Password=@p1,Name=@p2,Address=@p3,Email=@p4,Phone=@p5, ModifiedBy=@p6,ModifiedDate=@date,Status=@p8 where ID=@p9 ", ls);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public IEnumerable<User> ListAll(string searchString, int page, int pagesize)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return db.Users.SqlQuery("select * From [User] where UserName like '%" + searchString + "%'" +
                    " or Name like '%" + searchString + "%'").ToPagedList(page, pagesize);
            }
            return db.Users.SqlQuery("select * from [User]").ToPagedList(page,pagesize) ;
        }
        public bool Delete(int id)
        {
            return db.Database.ExecuteSqlCommand("delete from [User] where ID=@p0", id)!=null;
        }
      
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            if (user.Status == true)
            {
                user.Status = false;
            }
            else
            {
                user.Status = true;
            }          
            db.SaveChanges();
            return user.Status;
        }
    }
}

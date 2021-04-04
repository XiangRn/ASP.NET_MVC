using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
namespace Models
{
    public class AccountModel
    {
        public OnlineShopDBContext context = null;
        public AccountModel()
        {
            context = new OnlineShopDBContext();
        }
        public string Login(string username, string password)
        {
           //object[] sqlParams = new SqlParameter[]
           // {
           //     new SqlParameter("@UserName",username),
           // new SqlParameter("@PassWord",password)
           // };
            //Trả về giá trị duy nhất thì dùng singleOrDefault
            var res = context.Database.SqlQuery<string>("Select studentname from Account where UserName=@username and PassWord=@password", new SqlParameter("@username", username),
       new SqlParameter("@password", password)).SingleOrDefault();

            return res;
        }
    }
}

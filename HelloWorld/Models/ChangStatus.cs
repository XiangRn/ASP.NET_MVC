using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Framework;

namespace HelloWorld.Models
{
    public class ChangStatus
    {
        OnlineShopDBContext db = null;
        public ChangStatus()
        {
            db = new OnlineShopDBContext();
        }
        public bool Change(long id)
        {
            var result = db.Users.Find(id).Status;
            if (result)
            {
                db.Users.Find(id).Status = false;
                db.SaveChanges();
                return false;
            }
            else
            {
                db.Users.Find(id).Status = true;
                db.SaveChanges();
                return true;
            }
        }
    }
}
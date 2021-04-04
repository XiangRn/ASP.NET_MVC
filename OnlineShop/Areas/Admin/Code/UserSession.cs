using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Code
{
    [Serializable]//tuần tự hóa ra nhị phân
    public class UserSession
    {
      public long Id { get; set; }
        public string UserName { get; set; }
     
    }
}
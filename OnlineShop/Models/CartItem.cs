using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Framework;
namespace OnlineShop.Models
{
   
    public class CartItem
    {
        public Productt Product { get; set; }
        public int Quantity { get; set; }
    }
}
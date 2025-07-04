using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ministore.Models
{
    public class Cartitem
    {
        public int productid { get; set; }
        public int productname { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }
    }
}
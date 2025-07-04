using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ministore.Models
{
    public class Listofproducts
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public string images { get; set; }
    }

}
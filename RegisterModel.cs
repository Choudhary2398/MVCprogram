using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace second_Mvc_Web_Apps.Models
{
    public class RegisterModel
    {
        public string fullname {  get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime regdate { get; set; }
        public int countryid  { get; set; }
        public string countryname { get; set; }
    }
}
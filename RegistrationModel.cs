using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jquerywithajax.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
     

        public int CountryID { get; set; }

        public CountryModel Country { get; set; }
    }
}
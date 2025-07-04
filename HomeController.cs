using jquerywithajax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jquerywithajax.Controllers
{
    public class HomeController : Controller
    {
        Logic repo = new Logic();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var data = repo.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(RegistrationModel reg)
        {
            string action = reg.Id == 0 ? "Insert" : "Update";
            repo.Save(reg, action);
            return Json(new { status = "Success" });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            repo.Delete(id);
            return Json(new { status = "Deleted" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emailtestprogram.Controllers
{
    public class EmailController: Controller
    {
        public ActionResult Index()
        {
            var emailService = new Email_logic();
            string error;

            if (emailService.SendEmail(out error))
            {
                ViewBag.Message = "✅ Email sent successfully!";
            }
            else
            {
                ViewBag.Message = "❌ Failed to send email: " + error;
            }

            return View();
        }
    }
}
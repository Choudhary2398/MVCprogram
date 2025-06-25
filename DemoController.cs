using second_Mvc_Web_Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace second_Mvc_Web_Apps.Controllers
{
    public class DemoController : Controller
    {
        Logic lo = new Logic();

        public ActionResult Index()
        {
            List<ListofEmployees> employees = lo.GetAll();
            var emailService = new Logic();
            string error;
           /* if (emailService.SendEmail(out error))
            {
                TempData["Message"] = "✅ Email sent successfully!";
            }
            else
            {
                TempData["Message"] = "❌ Failed to send email: " + error;
            }*/

            return View(employees);
            


        }
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(ListofEmployees insertemployee, HttpPostedFileBase imageFile )
        {
            try
            {
                var emailexists = lo.Duplicate_email_checking(insertemployee.email);
                if (emailexists)
                {
                    ModelState.AddModelError("email","this email is already exists");
                }
                var emailService = new Logic();
                string error;
                if (emailService.SendEmail(out error))
                {
                    TempData["Message"] = "✅ Email sent successfully!";
                }
                else
                {
                    TempData["Message"] = "❌ Failed to send email: " + error;
                }
                if (ModelState.IsValid)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string serverPath = Server.MapPath("~/Content/assets/");
                        string relativePath = "/Content/assets/";

                        string savedPath = lo.SaveImage(imageFile, serverPath, relativePath);
                        insertemployee.Images = savedPath;
                    }
                    lo.Insert(insertemployee);
                    return RedirectToAction("Index");
                }

            else
                {
                    return View(insertemployee);
                }

            }
            catch (SqlException ex)
            {
                // Check if it’s your custom SQL error
                Console.WriteLine(ex.Message);
                return View(insertemployee);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            List<ListofEmployees> employees = lo.GetAll();
            ListofEmployees employee = employees.FirstOrDefault(s => s.empid == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(ListofEmployees employees , HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string serverPath = Server.MapPath("~/Content/assets/");
                    string relativePath = "/Content/assets/";

                    // ✅ Call your helper method
                    string savedPath = lo.SaveImage(imageFile, serverPath, relativePath);
                    employees.Images = savedPath;
                }
                lo.Update(employees);
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        public ActionResult Delete(int id)
        {
             lo.Delete(id);
             return RedirectToAction("Index");
        }
         
       


        [HttpGet]

        public ActionResult Details(int id)
        {
            var employees = lo.GetAll().Find(emp => emp.empid == id);
            return View(employees);
        }

       



    }
         
    
}



  
            
using second_Mvc_Web_Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;

namespace second_Mvc_Web_Apps.Controllers
{
    public class DemoController : Controller
    {
        Logic lo = new Logic();

        public ActionResult Index()
        {
            List<ListofEmployees> employees = lo.GetAll();
            return View(employees);
        }
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(ListofEmployees employees)
        {
            if (ModelState.IsValid)
            {
                lo.Insert(employees);
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            List<ListofEmployees> employees = lo.GetAll();
            ListofEmployees employee = employees.FirstOrDefault(s => s.empid == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(ListofEmployees employees)
        {
            if (ModelState.IsValid)
            {
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



  
            
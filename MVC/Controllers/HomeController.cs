using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            MVCEntities db = new MVCEntities();
            Employee emp = new Employee();

            var lastemp = db.Employees.OrderByDescending(c => c.id).FirstOrDefault();

            if (lastemp == null)
            {
                emp.Employee_id = "001";
            }
            else
            {
                emp.Employee_id = (Convert.ToInt32(lastemp.Employee_id) + 1).ToString("D3");
            }

            List<Department> dplist = db.Departments.ToList();
            ViewBag.List = new SelectList(dplist, "D_id", "D_Name");


            EmpVM empvm = new EmpVM();
            empvm.Employee_id = emp.Employee_id;
            


            return View(empvm);
        }


        [HttpPost]
        public ActionResult Index(EmpVM e)
        {
            if (ModelState.IsValid)
            { 
            MVCEntities db = new MVCEntities();

            Employee emp = new Employee();
            emp.Employee_id = e.Employee_id;
            emp.Name = e.Name;
            emp.Address = e.Address;
            emp.d_id = e.d_id;

            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View();
        }
    }
}
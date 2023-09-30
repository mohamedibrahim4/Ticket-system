using Platform_Task__Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform_Task__Management_System.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.numberbranches = db.Branches.Count();
            ViewBag.numberFacility = db.Facilities.Count();
            ViewBag.numberFacilitySupported = db.Facilities.Where(c=>c.IsSupport==true).Count();
            ViewBag.numberFacilityNotSupported = db.Facilities.Where(c => c.IsSupport == false).Count();


            ViewBag.numberSoftware = db.Softwares.Count();

            ViewBag.numberTask = db.Tasks.Count();
            ViewBag.numberTaskComplete = db.Tasks.Where(c=>c.status==0).Count();
            ViewBag.numberTaskPending = db.Tasks.Where(c => c.status == (Status)1).Count();
            ViewBag.numberTaskInProgress = db.Tasks.Where(c => c.status == (Status)2).Count();
            ViewBag.numberTaskCanceled = db.Tasks.Where(c => c.status == (Status)3).Count();
            //ViewBag.numberTaskByEngMostafa = db.Tasks.Where(c => c.applicationUserID == "0455fd6d-a7ae-46e6-b234-80e5817b8a7a").Count();
            //ViewBag.numberTaskByRamdan = db.Tasks.Where(c => c.applicationUserID == "0a1cfa54-dff8-4100-a403-122156dbb346").Count();
            Dictionary<string, int> My_dict =
                  new Dictionary<string, int>();
            foreach (var item in db.Users.Where(c => c.IsActive == true).ToList())

            {
                string name = item.UserName;

                int count = db.Tasks.Where(c => c.applicationUserID == item.Id).Count();
               
                My_dict.Add(name, count);

            }
            ViewData["dataa"] = My_dict;

            foreach (var item in (Dictionary<string, int>)ViewData["dataa"])
            {
                item.Key.ToString();
                item.Value.ToString();
            }
            ViewBag.counter = db.Users.Where(c => c.IsActive == true).ToList();










            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
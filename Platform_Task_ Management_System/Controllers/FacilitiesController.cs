using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Platform_Task__Management_System.Models;

namespace Platform_Task__Management_System.Controllers
{
    [Authorize]

    public class FacilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facilities
        public ActionResult Index()
        {
            ViewBag.numberFacility = db.Facilities.Count();

            return View(db.Facilities.Include(b=>b.branch).ToList());
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "TeamLeader")]
        public JsonResult CreateNewFacility(Facility model)
        {

            if (model != null)
            {


                db.Facilities.Add(model);
                db.SaveChanges();

                return Json("Facility CREATE SUCCessfully");


            }
            return Json("Addedd failed");
        }
        [HttpGet]
        public JsonResult getBranchesDropdown()
        {

            List<Branch> model = new List<Branch>();
            model = db.Branches.ToList();
            string s = string.Empty;
            s = JsonConvert.SerializeObject(model,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return Json(s, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult GetAllFacilities()
        {

            List<Facility> model = new List<Facility>();
            model = db.Facilities.ToList();
            //return Json(model, JsonRequestBehavior.AllowGet);

            string s = string.Empty;
            s = JsonConvert.SerializeObject(model,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });

            return Json(s, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult DeletFacilityDeltails(int id)
        {


            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility);
            db.SaveChanges();
            return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult GetFacilityNameById(int id)
        {


            string FacilityName = db.Facilities.Find(id).FacilityName;
            Facility b = db.Facilities.Find(id);

            string s = string.Empty;
            s = JsonConvert.SerializeObject(b,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return Json(s, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]

        public JsonResult UpdateFacilityDetailById(int id, string NewFacilityName, string NewlicenseNumber, string NewCodeFile, int NewADT, int NewPPR, int NewOMP, int NewRAS, string NewTelephoneNumber, string NewPasswordServer,
           string NewAnyDeskID, string NewAnyDeskPassword, bool NewIsSupport,string NewComment, int NewBranchID)
        {

            if (id != null)
            {

                string OldFacilityName = db.Facilities.Find(id).FacilityName;

                db.Facilities.Find(id).FacilityName = NewFacilityName;
                db.Facilities.Find(id).licenseNumber = NewlicenseNumber;
                db.Facilities.Find(id).CodeFile = NewCodeFile;
                db.Facilities.Find(id).ADT = NewADT;
                db.Facilities.Find(id).PPR = NewPPR;
                db.Facilities.Find(id).OMP = NewOMP;
                db.Facilities.Find(id).RAS = NewRAS;
                db.Facilities.Find(id).TelephoneNumber = NewTelephoneNumber;
                db.Facilities.Find(id).PasswordServer = NewPasswordServer;
                db.Facilities.Find(id).AnyDeskID = NewAnyDeskID;
                db.Facilities.Find(id).AnyDeskPassword = NewAnyDeskPassword;
                db.Facilities.Find(id).IsSupport = NewIsSupport;
                db.Facilities.Find(id).Comment = NewComment;

                db.Facilities.Find(id).BranchID = NewBranchID;
                db.SaveChanges();

                return Json("Facility Update Scccessfully");


            }
            return Json("Update failed");
        }

        // GET: Facilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: Facilities/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "BranchID", "BranchName");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacilityID,FacilityName,licenseNumber,CodeFile,ADT,PPR,OMP,RAS,TelephoneNumber,PasswordServer,AnyDeskID,AnyDeskPassword,IsSupport")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                db.Facilities.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facility);
        }

        // GET: Facilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacilityID,FacilityName,licenseNumber,CodeFile,ADT,PPR,OMP,RAS,TelephoneNumber,PasswordServer,AnyDeskID,AnyDeskPassword,IsSupport")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facility);
        }

        // GET: Facilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

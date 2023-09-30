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

    public class SoftwaresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Softwares
        public ActionResult Index()
        {
            ViewBag.numberSoftwares = db.Softwares.Count();

            return View(db.Softwares.ToList());
        }

        //// GET: Softwares/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Software software = db.Softwares.Find(id);
        //    if (software == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(software);
        //}

        //// GET: Softwares/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Softwares/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "SoftwareID,SoftwareName")] Software software)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Softwares.Add(software);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(software);
        //}

        //// GET: Softwares/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Software software = db.Softwares.Find(id);
        //    if (software == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(software);
        //}

        //// POST: Softwares/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SoftwareID,SoftwareName")] Software software)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(software).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(software);
        //}

        //// GET: Softwares/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Software software = db.Softwares.Find(id);
        //    if (software == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(software);
        //}

        //// POST: Softwares/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Software software = db.Softwares.Find(id);
        //    db.Softwares.Remove(software);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "TeamLeader")]
        public JsonResult CreateNewSoftware(Software model)
        {

            if (model != null)
            {


                db.Softwares.Add(model);
                db.SaveChanges();

                return Json("Software CREATE SUCCessfully");


            }
            return Json("ADdedd failed");
        }
        [HttpGet]
        public JsonResult GetAllSoftwares()
        {

            List<Software> model = new List<Software>();
            model = db.Softwares.ToList();
            string s = string.Empty;
            s = JsonConvert.SerializeObject(model,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return Json(s, JsonRequestBehavior.AllowGet);


        }
        [HttpGet]
        public JsonResult DeleteSoftware(int id)
        {


            Software software = db.Softwares.Find(id);
            db.Softwares.Remove(software);
            db.SaveChanges();
            return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult GetSoftwareNameById(int id)
        {


            string SoftwareName = db.Softwares.Find(id).SoftwareName;
            Software b = db.Softwares.Find(id);

            string s = string.Empty;
            s = JsonConvert.SerializeObject(b,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return Json(s, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "TeamLeader")]
        public JsonResult UpdatSoftwareNameById(int id, string NewSoftwareName)
        {

            if (id != null)
            {

                string OldSoftwareName = db.Softwares.Find(id).SoftwareName;

                db.Softwares.Find(id).SoftwareName = NewSoftwareName;
                db.SaveChanges();

                return Json("Branch Update SUCCessfully");


            }
            return Json("Update failed");
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

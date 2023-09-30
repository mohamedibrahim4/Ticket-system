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

    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Branches
        public ActionResult Index()
        {
            ViewBag.numberbranches = db.Branches.Count();

            return View(db.Branches.ToList());
        }

        // GET: Branches/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Branch branch = db.Branches.Find(id);
        //    if (branch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(branch);
        //}

        //// GET: Branches/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Branches/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "BranchID,BranchName")] Branch branch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Branches.Add(branch);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(branch);
        //}

        //// GET: Branches/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Branch branch = db.Branches.Find(id);
        //    if (branch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(branch);
        //}

        //// POST: Branches/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BranchID,BranchName")] Branch branch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(branch).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(branch);
        //}

        //// GET: Branches/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Branch branch = db.Branches.Find(id);
        //    if (branch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(branch);
        //}

        //// POST: Branches/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Branch branch = db.Branches.Find(id);
        //    db.Branches.Remove(branch);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "TeamLeader")]
        public JsonResult CreateNewBranch(Branch model)
        {

            if (model != null)
            {


                db.Branches.Add(model);
                db.SaveChanges();

                return Json("Branch CREATE SUCCessfully");


            }
            return Json("ADdedd failed");
        }
        [HttpGet]
        public JsonResult GetAllBranches()
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
        public JsonResult DeleteBrach(int id)
        {


            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            db.SaveChanges();
            return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public JsonResult GetBranchNameById(int id)
        {


            string BranchName = db.Branches.Find(id).BranchName;
            Branch b = db.Branches.Find(id);
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
        public JsonResult UpdateBranchNameById(int id, string NewBranchName)
        {

            if (id != null)
            {

                string OldBranchName = db.Branches.Find(id).BranchName;

                db.Branches.Find(id).BranchName = NewBranchName;
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

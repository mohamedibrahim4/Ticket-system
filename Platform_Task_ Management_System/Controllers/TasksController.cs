using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Platform_Task__Management_System.Models;

namespace Platform_Task__Management_System.Controllers
{
    [Authorize]

    public class TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tasks
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();

            ViewBag.numberTask = db.Tasks.Where(t => t.applicationUserID == id).Count();

            var tasks = db.Tasks.Where(t => t.applicationUserID == id).Include(t => t.applicationUser).Include(t => t.facility).Include(t => t.software);
            return View(tasks.ToList());
        }
        public ActionResult IndexTotal()
        {
            string id = User.Identity.GetUserId();

            ViewBag.numberTask = db.Tasks.Count();

            var tasks = db.Tasks.Include(t => t.applicationUser).Include(t => t.facility).Include(t => t.software);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.applicationUserID = new SelectList(db.Users.Where(c => c.IsActive == true).ToList(), "Id", "ProgramName") ;
            ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName");
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName");
            return View();
        }

        [HttpGet]
        public JsonResult Notify(int id)
        {

            if (id != null)
            {
                Task t = db.Tasks.Find(id);
            }
                return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
 
        public JsonResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                string NewFilesPath = string.Empty;
                string NewContentType = string.Empty;

                //PathAndContent p = new PathAndContent();
                //task.pathAndContents = new List<PathAndContent>();

                foreach (HttpPostedFileBase file in task.files)
                {
                    //Checking file is available to save.
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.
                        ViewBag.UploadStatus = task.files.Count().ToString() + " files uploaded successfully.";
                        NewFilesPath +="!!!" +ServerSavePath;
                        NewContentType += "***" + file.ContentType;
                        //p.FilePath = ServerSavePath;
                        //p.ContentType = file.ContentType;
                        ////p.TaskID = task.TaskID;

                        //task.pathAndContents.Add(p);


                        //task.pathAndContents.Add(new PathAndContent { FilePath = ServerSavePath, ContentType = file.ContentType });
                    }


                }

                task.FilePath = NewFilesPath;
                task.ContentType = NewContentType;
                //task.applicationUserID = User.Identity.GetUserId();
                //task.applicationUserID = db.Tasks.Where(t => t.applicationUserID == task.applicationUserID).ToString();

                db.Tasks.Add(task);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", task.applicationUserID);
            ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", task.FacilityID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", task.SoftwareID);
            RedirectToAction("index");
            return Json("");
        }

        public ActionResult DownloadFile(string filePath)
        {
            string fullName = filePath;
                //Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        // GET: Tasks/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", task.applicationUserID);
            ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", task.FacilityID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", task.SoftwareID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,int status ,DateTime DeadLineDate, DateTime CreationDate,string applicationUserID,string Notes)
            //[Bind(Include = "TaskID,TaskName,TaskRequirements,DeadLineDate,CreationDate,FilesPath,ContentType,FacilityID,applicationUserID,status,SoftwareID")]
        //Task task)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(task).State = EntityState.Modified;
                db.Tasks.Find(id).status = (Status)status;
                db.Tasks.Find(id).DeadLineDate = DeadLineDate;
                db.Tasks.Find(id).CreationDate = CreationDate;
                db.Tasks.Find(id).applicationUserID = applicationUserID;
                db.Tasks.Find(id).Notes = Notes;



                db.SaveChanges();
                return RedirectToAction("IndexTotal");
            }
            //////ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", task.applicationUserID);
            //////ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", task.FacilityID);
            //////ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", task.SoftwareID);
            return View();
        }
        [HttpGet]
        public JsonResult DeleteTask(int id)
        {


            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }
        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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

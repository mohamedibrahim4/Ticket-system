//using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Platform_Task__Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Platform_Task__Management_System.Controllers
{
    [Authorize]

    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            string id= User.Identity.GetUserId();

            ViewBag.numberTicket = db.Tickets.Where(t => t.applicationUserID == id).Count();
            var tickets = db.Tickets.Where(t=>t.applicationUserID==id).Include(t => t.applicationUser).Include(t => t.software);


            //var tickets = db.Tickets.Include(t => t.applicationUser).Include(t => t.software);
            return View(tickets.ToList());
        }
        public ActionResult IndexTotal()
        {
            string id = User.Identity.GetUserId();

            ViewBag.numberTicket = db.Tickets.Count();
            var tickets = db.Tickets.Include(t => t.applicationUser).Include(t => t.software);


            //var tickets = db.Tickets.Include(t => t.applicationUser).Include(t => t.software);
            return View(tickets.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.applicationUserID = new SelectList(db.Users.Where(c => c.IsActive == null).ToList(), "Id", "ProgramName");
            ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName");
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName");
            return View();
        }

        [HttpGet]
        public JsonResult Notify(int id)
        {

            if (id != null)
            {
                Ticket t = db.Tickets.Find(id);
            }
            return Json("success Deleted", JsonRequestBehavior.AllowGet);


        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public JsonResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                string NewFilesPath = string.Empty;
                string NewContentType = string.Empty;

           

                foreach (HttpPostedFileBase file in ticket.files)
                {
                    //Checking file is available to save.
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/TicketPIc/") + InputFileName);
                        //Save file to server folder
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.
                        ViewBag.UploadStatus = ticket.files.Count().ToString() + " files uploaded successfully.";
                        NewFilesPath += "!!!" + ServerSavePath;
                        NewContentType += "***" + file.ContentType;
                    
                    }


                }

                ticket.FilePath = NewFilesPath;
                ticket.ContentType = NewContentType;
                ticket.status = Status.Pending;
                ticket.CreationDate = DateTime.Now;
                ticket.applicationUserID= User.Identity.GetUserId();
                //ticket.applicationUserID = User.Identity.GetUserId();

                db.Tickets.Add(ticket);
                db.SaveChanges();
                // return RedirectToAction("Index");
            }

            ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", ticket.applicationUserID);
            //ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", ticket.FacilityID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", ticket.SoftwareID);
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
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", ticket.applicationUserID);
            //ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", ticket.FacilityID);
            ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", ticket.SoftwareID);
            return View(ticket);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int status)
        //[Bind(Include = "TaskID,TaskName,TaskRequirements,DeadLineDate,CreationDate,FilesPath,ContentType,FacilityID,applicationUserID,status,SoftwareID")]
        //Task task)
        {
            if (ModelState.IsValid) 
            {
                //db.Entry(task).State = EntityState.Modified;
                db.Tickets.Find(id).status = (Status)status;
                


                db.SaveChanges();
                return RedirectToAction("IndexTotal");
            }
            //////ViewBag.applicationUserID = new SelectList(db.Users, "Id", "ProgramName", task.applicationUserID);
            //////ViewBag.FacilityID = new SelectList(db.Facilities, "FacilityID", "FacilityName", task.FacilityID);
            //////ViewBag.SoftwareID = new SelectList(db.Softwares, "SoftwareID", "SoftwareName", task.SoftwareID);
            return View();
        }
        [HttpGet]
        public JsonResult DeleteTicket(int id)
        {


            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
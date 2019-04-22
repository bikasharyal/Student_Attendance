using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using student_attendance.Models;

namespace student_attendance.Controllers
{
    
    public class AttendancesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.schedule_id_fk).Include(a => a.student_id_fk);
            //ViewBag.schedules = db.Schedules.SqlQuery("SELECT * from schedules INNER JOIN groups ON groups.group_id = schedules.group_id").ToList();
            ViewBag.schedules = db.Schedules.SqlQuery("SELECT * from schedules").ToList();
            ViewBag.groups = db.Groups.SqlQuery("SELECT * from groups").ToList();
            ViewBag.modules = db.Modules.SqlQuery("SELECT * from modules").ToList();
            /*ViewBag.schedules = (from schedules in db.Schedules
                                 join groups in db.Groups on schedules.group_id equals groups.group_id
                                 join modules in db.Modules on schedules.module_id equals modules.module_id
                                 select groups.name).ToList().ToArray();*/
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            if (Request.QueryString["schedule_id"] != null)
            {
                ViewBag.get_schedule_id = Request.QueryString["schedule_id"];
                ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day");
                ViewBag.student_id = new SelectList(db.Students, "student_id", "name");
                var rows = db.Students.SqlQuery("SELECT * from students").ToList().ToArray();
                ViewBag.rows = rows;

                /*foreach(var a in rows)
                {
                    Response.Write(a.name);
                }*/

                return View();
            }
            else
            {
                Response.Write("Missing Schedule Id.....Redirecting Back..<script>setTimeout(function(){location.href='/Attendances'}, 1000);</script>");
                return null;
            }
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "attendance_id,date,entry_time,status,student_id,schedule_id")] Attendance attendance)
        {
            Response.Write("date " + attendance.date + "<br/>");
            Response.Write("entry_time " + attendance.entry_time + "<br/>");
            Response.Write("status (array)" + attendance.status + "<br/>");
            Response.Write("student_id (array)" + attendance.student_id + "<br/>");
            Response.Write("schedule_id" + attendance.schedule_id + "<br/>");

            Response.Write(attendance.student_id.GetType());
            /*String[] allStatus = Request.Form.GetValues("status");
            String[] allStudentId = Request.Form.GetValues("student_id");

            int i = 0;
            foreach(string status in allStatus)
            {
                Response.Write("Status: " + status + " - of: " + allStudentId[i] + "<br/>");
                i++;
            }*/
            

            return null;

            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", attendance.schedule_id);
            ViewBag.student_id = new SelectList(db.Students, "student_id", "name", attendance.student_id);
            return Index();
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", attendance.schedule_id);
            ViewBag.student_id = new SelectList(db.Students, "student_id", "name", attendance.student_id);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "attendance_id,date,entry_time,status,student_id,schedule_id")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", attendance.schedule_id);
            ViewBag.student_id = new SelectList(db.Students, "student_id", "name", attendance.student_id);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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

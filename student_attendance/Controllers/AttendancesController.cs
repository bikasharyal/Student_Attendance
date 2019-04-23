using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
            IQueryable<Attendance> attendances;
            int id;
            DateTime date;

            if(Request.QueryString["schedule_id"] == "" || Request.QueryString["date"] == "")
            {
                TempData["error"] = "Schedule Id or Date was Invalid";
            }

            if (Request.QueryString["schedule_id"] == null || Request.QueryString["schedule_id"] == "" || Request.QueryString["date"] == null || Request.QueryString["date"] == "")
            {
                //attendances = db.Attendances.Include(a => a.schedule_id_fk).Include(a => a.student_id_fk).OrderByDescending(a => a.date).Take(0);
                attendances = Enumerable.Empty<Attendance>().AsQueryable();
            }
            else
            {
                id = Convert.ToInt32(Request.QueryString["schedule_id"]);
                date = DateTime.ParseExact(Request.QueryString["date"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                attendances = db.Attendances.Include(a => a.schedule_id_fk).Include(a => a.student_id_fk).Where(a => a.schedule_id == id).Where(a => a.date == date).OrderBy(a => a.student_id_fk.name);
                ViewBag.schedule_id = id;
                ViewBag.date = date;
            }

            //ViewBag.schedules = db.Schedules.SqlQuery("SELECT * from schedules INNER JOIN groups INNER JOIN modules").ToList().ToArray();
            ViewBag.schedules = (from s in db.Schedules
                                 join g in db.Groups on s.group_id equals g.group_id
                                 join m in db.Modules on s.module_id equals m.module_id
                                 orderby g.name
                                 orderby m.module_name
                                 orderby s.class_type
                                 select s).ToList().ToArray();

            /*ViewBag.groups = db.Groups.SqlQuery("SELECT * from groups").ToList();
            ViewBag.modules = db.Modules.SqlQuery("SELECT * from modules").ToList();*/
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
                int schedule_id_for = Convert.ToInt32(Request.QueryString["schedule_id"]);
                if (Request.QueryString["select_date"] != null && Request.QueryString["select_date"] != "")
                {
                    DateTime date_for = DateTime.ParseExact(Request.QueryString["select_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    ViewBag.date_for = date_for;
                }
                else
                {
                    DateTime date_for = DateTime.ParseExact(DateTime.Today.ToShortDateString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    ViewBag.date_for = date_for;
                }
                ViewBag.get_schedule_id = Request.QueryString["schedule_id"];
                ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day");
                ViewBag.student_id = new SelectList(db.Students, "student_id", "name");
                //var rows = db.Students.SqlQuery("SELECT * from students").ToList().ToArray();
                var rows = (from s in db.Students
                            join g in db.Groups on s.group_id equals g.group_id
                            join sc in db.Schedules on g.group_id equals sc.group_id
                            where sc.schedule_id == schedule_id_for
                            orderby s.name
                            select s).ToList().ToArray();
                ViewBag.rows = rows;

                var rows_sc = db.Schedules.SqlQuery("SELECT * from schedules where schedule_id='" + Request.QueryString["schedule_id"] + "'").ToList().ToArray();
                
                ViewBag.rows_sc = rows_sc;

                return View();
            }
            else
            {
                //Response.Write("Missing Schedule Id.....Redirecting Back..<script>setTimeout(function(){location.href='/Attendances'}, 1000);</script>");
                //return null;
                TempData["error"] = "Schedule Id not Provided.";
                return RedirectToAction("Index");
            }
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attendance attendance)
        {
            var attendance_already = db.Attendances.SqlQuery("SELECT * from attendances where date = '" + attendance.date + "' and schedule_id = '" + attendance.schedule_id + "'").ToList().ToArray();
            if (attendance_already.Length == 0)
            {

                var schedule_info = db.Schedules.SqlQuery("SELECT * from schedules where schedule_id = '" + attendance.schedule_id + "'").ToList().ToArray();
                DateTime start_time = schedule_info[0].start_time;

                String[] allStatus = Request.Form.GetValues("status");
                String[] allStudentId = Request.Form.GetValues("student_id");

                /*Response.Write(Request.Form.GetValues("student_id")[0] + "<br/>");
                Response.Write(Request.Form.GetValues("status")[0] + "<br/>");
                Response.Write("schedule_id" + attendance.schedule_id + "<br/>");*/

                if(allStatus == null || allStatus.Length <= 0)
                {
                    TempData["message"] = "Attendance for Zero Students. Epic.";

                    ViewBag.schedule_id = attendance.schedule_id;
                    return RedirectToAction("Index", new { schedule_id = attendance.schedule_id });
                }

                int i = 0;
                foreach (string stat in allStatus)
                {
                    DateTime entry_time = attendance.entry_time;

                    var stat_verified = "0";
                    if (stat != "0")
                    {
                        if ((entry_time.TimeOfDay - start_time.TimeOfDay).TotalMinutes <= 15)
                        {
                            stat_verified = "1";
                        }
                        else
                        {
                            stat_verified = "2";
                        }
                    }

                    db.Attendances.Add(new Attendance
                    {
                        date = attendance.date,
                        entry_time = attendance.entry_time,
                        status = stat_verified,
                        student_id = Convert.ToInt32(allStudentId[i]),
                        schedule_id = attendance.schedule_id
                    });
                    db.SaveChanges();
                    i++;
                }

                TempData["message"] = "Attendance Recorded Successfully";

                ViewBag.schedule_id = attendance.schedule_id;
                return RedirectToAction("Index", new { schedule_id = attendance.schedule_id, date = attendance.date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) });
            }
            else
            {
                TempData["error"] = "Attendance Was Already Taken for Selected Date - " + attendance.date.ToShortDateString();
                return RedirectToAction("Create", new { schedule_id = attendance.schedule_id, select_date = attendance.date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) });
            }
            /*if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "day", attendance.schedule_id);
            ViewBag.student_id = new SelectList(db.Students, "student_id", "name", attendance.student_id);
            return Index();*/
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            Schedule schedule = db.Schedules.Find(attendance.schedule_id);

            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.schedule_id = new SelectList(db.Schedules, "schedule_id", "course_id_fk.name", attendance.schedule_id);
            ViewBag.student_id = new SelectList(db.Students, "student_id", "name", attendance.student_id);
            //ViewBag.schedules = db.Schedules.SqlQuery("SELECT * from schedules").ToList();

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

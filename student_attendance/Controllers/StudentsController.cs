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
    public class StudentsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.course_id_fk).Include(s => s.group_id_fk).Include(s => s.user_id_fk);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "course_name");
            ViewBag.group_id = new SelectList(db.Groups, "group_id", "name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,name,gender,email,phone_no,dob,enrolled_date,user_id,course_id,group_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_id = new SelectList(db.Courses, "course_id", "course_name", student.course_id);
            ViewBag.group_id = new SelectList(db.Groups, "group_id", "name", student.group_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", student.user_id);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "course_name", student.course_id);
            ViewBag.group_id = new SelectList(db.Groups, "group_id", "name", student.group_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", student.user_id);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,name,gender,email,phone_no,dob,enrolled_date,user_id,course_id,group_id")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "course_name", student.course_id);
            ViewBag.group_id = new SelectList(db.Groups, "group_id", "name", student.group_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", student.user_id);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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

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
    public class Teacher_ModuleController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Teacher_Module
        public ActionResult Index()
        {
            var teacher_Modules = db.Teacher_Modules.Include(t => t.module_id_fk).Include(t => t.teacher_id_fk);
            return View(teacher_Modules.ToList());
        }

        // GET: Teacher_Module/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            if (teacher_Module == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Create
        public ActionResult Create()
        {
            ViewBag.module_id = new SelectList(db.Modules, "module_id", "module_code");
            ViewBag.teacher_id = new SelectList(db.Teachers, "teacher_id", "name");
            return View();
        }

        // POST: Teacher_Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,teacher_id,module_id,teacher_type")] Teacher_Module teacher_Module)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Modules.Add(teacher_Module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.module_id = new SelectList(db.Modules, "module_id", "module_code", teacher_Module.module_id);
            ViewBag.teacher_id = new SelectList(db.Teachers, "teacher_id", "name", teacher_Module.teacher_id);
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            if (teacher_Module == null)
            {
                return HttpNotFound();
            }
            ViewBag.module_id = new SelectList(db.Modules, "module_id", "module_code", teacher_Module.module_id);
            ViewBag.teacher_id = new SelectList(db.Teachers, "teacher_id", "name", teacher_Module.teacher_id);
            return View(teacher_Module);
        }

        // POST: Teacher_Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,teacher_id,module_id,teacher_type")] Teacher_Module teacher_Module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.module_id = new SelectList(db.Modules, "module_id", "module_code", teacher_Module.module_id);
            ViewBag.teacher_id = new SelectList(db.Teachers, "teacher_id", "name", teacher_Module.teacher_id);
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            if (teacher_Module == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Module);
        }

        // POST: Teacher_Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            db.Teacher_Modules.Remove(teacher_Module);
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

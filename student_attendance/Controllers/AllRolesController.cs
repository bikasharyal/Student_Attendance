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
    public class AllRolesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AllRoles
        public ActionResult Index()
        {
            return View(db.AllRoles.ToList());
        }

        // GET: AllRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllRole allRole = db.AllRoles.Find(id);
            if (allRole == null)
            {
                return HttpNotFound();
            }
            return View(allRole);
        }

        // GET: AllRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "role_id,role")] AllRole allRole)
        {
            if (ModelState.IsValid)
            {
                db.AllRoles.Add(allRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allRole);
        }

        // GET: AllRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllRole allRole = db.AllRoles.Find(id);
            if (allRole == null)
            {
                return HttpNotFound();
            }
            return View(allRole);
        }

        // POST: AllRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "role_id,role")] AllRole allRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allRole);
        }

        // GET: AllRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllRole allRole = db.AllRoles.Find(id);
            if (allRole == null)
            {
                return HttpNotFound();
            }
            return View(allRole);
        }

        // POST: AllRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllRole allRole = db.AllRoles.Find(id);
            db.AllRoles.Remove(allRole);
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

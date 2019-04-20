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
    public class UserAllRolesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: UserAllRoles
        public ActionResult Index()
        {
            var userAllRoles = db.UserAllRoles.Include(u => u.role_id_fk).Include(u => u.user_id_fk);
            return View(userAllRoles.ToList());
        }

        // GET: UserAllRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAllRole userAllRole = db.UserAllRoles.Find(id);
            if (userAllRole == null)
            {
                return HttpNotFound();
            }
            return View(userAllRole);
        }

        // GET: UserAllRoles/Create
        public ActionResult Create()
        {
            ViewBag.role_id = new SelectList(db.AllRoles, "role_id", "role");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }

        // POST: UserAllRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_role_id,user_id,role_id")] UserAllRole userAllRole)
        {
            if (ModelState.IsValid)
            {
                db.UserAllRoles.Add(userAllRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.role_id = new SelectList(db.AllRoles, "role_id", "role", userAllRole.role_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", userAllRole.user_id);
            return View(userAllRole);
        }

        // GET: UserAllRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAllRole userAllRole = db.UserAllRoles.Find(id);
            if (userAllRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.role_id = new SelectList(db.AllRoles, "role_id", "role", userAllRole.role_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", userAllRole.user_id);
            return View(userAllRole);
        }

        // POST: UserAllRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_role_id,user_id,role_id")] UserAllRole userAllRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAllRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.role_id = new SelectList(db.AllRoles, "role_id", "role", userAllRole.role_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", userAllRole.user_id);
            return View(userAllRole);
        }

        // GET: UserAllRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAllRole userAllRole = db.UserAllRoles.Find(id);
            if (userAllRole == null)
            {
                return HttpNotFound();
            }
            return View(userAllRole);
        }

        // POST: UserAllRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAllRole userAllRole = db.UserAllRoles.Find(id);
            db.UserAllRoles.Remove(userAllRole);
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

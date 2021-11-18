using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LakeInn.Areas.Administrator.Common;
using LakeInn.Models.DataModels;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class UsersController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Administrator/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Administrator/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Password,Phone,Avatar,Status,Date_Created,Date_Updated")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.FullName == null)
                {
                    user.FullName = user.Email;
                }
                var chUn = db.Users.Where(x => x.Email == user.Email);
                if (chUn != null)
                {
                    TempData["error"] = "Email exist, please choose another email!";
                    return View(user);
                }
                var jp = user.Password + Constant.joinPass;
                var pass = Constant.EncodePassword(jp);
                user.Password = pass;
                user.Status = false;
                user.Forgot = true;
                user.Avatar = "/Areas/Administrator/TempLTE/dist/img/avatar.png";
                user.Date_Created = DateTime.Now;
                user.Date_Updated = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                TempData["success"] = "Create new user success!";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Administrator/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Administrator/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Password,Phone,Avatar,Status,Date_Created,Date_Updated")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Administrator/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Administrator/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult ChangeStatus(int id)
        {
            var u = db.Users.Find(id);
            u.Status = !u.Status;
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
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

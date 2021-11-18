using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LakeInn.Models.DataModels;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class TagsController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Tags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: Administrator/Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // GET: Administrator/Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TagName,Slug,Status,Date_Created,Date_Updated")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                tags.Slug = tags.TagName.Trim().ToLower();
                tags.Date_Created = DateTime.Now;
                tags.Date_Updated = DateTime.Now;
                db.Tags.Add(tags);
                db.SaveChanges();
                TempData["success"] = "Create tag successfully!";
                return RedirectToAction("Index");
            }

            return View(tags);
        }

        // GET: Administrator/Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // POST: Administrator/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TagName,Slug,Status,Date_Created,Date_Updated")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                var tag = db.Tags.Find(tags.Id);
                tag.TagName = tags.TagName;
                tag.Slug = tags.TagName.Trim().ToLower();
                tag.Status = tags.Status;
                tags.Date_Updated = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tags);
        }

        // GET: Administrator/Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // POST: Administrator/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tags tags = db.Tags.Find(id);
            db.Tags.Remove(tags);
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
        public JsonResult DeleteTag(int id)
        {
            Tags tags = db.Tags.Find(id);
            db.Tags.Remove(tags);
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

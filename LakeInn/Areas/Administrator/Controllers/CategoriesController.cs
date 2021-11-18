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
    public class CategoriesController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Administrator/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Administrator/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CateName,Status,Date_Created,Date_Updated")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Date_Created = DateTime.Now;
                category.Date_Updated = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["success"] = "Create category successfully!";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Administrator/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Administrator/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CateName,Status,Date_Created,Date_Updated")] Category category)
        {
            if (ModelState.IsValid)
            {
                var c = db.Categories.Find(category.Id);
                c.Status = category.Status;
                c.CateName = category.CateName;
                c.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit category successfully!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        
        public JsonResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
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

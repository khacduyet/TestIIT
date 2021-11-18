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
    public class BedTypesController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/BedTypes
        public ActionResult Index()
        {
            return View(db.BedTypes.ToList());
        }

        // GET: Administrator/BedTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BedType bedType = db.BedTypes.Find(id);
            if (bedType == null)
            {
                return HttpNotFound();
            }
            return View(bedType);
        }

        // GET: Administrator/BedTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/BedTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeBed,Description,Status,Date_Created,Date_Updated")] BedType bedType)
        {
            if (ModelState.IsValid)
            {
                bedType.Date_Created = DateTime.Now;
                bedType.Date_Updated = DateTime.Now;
                db.BedTypes.Add(bedType);
                db.SaveChanges();
                TempData["success"] = "Create bed type successfully!";
                return RedirectToAction("Index");
            }
            return View(bedType);
        }

        // GET: Administrator/BedTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BedType bedType = db.BedTypes.Find(id);
            if (bedType == null)
            {
                return HttpNotFound();
            }
            return View(bedType);
        }

        // POST: Administrator/BedTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeBed,Description,Status,Date_Created,Date_Updated")] BedType bedType)
        {
            if (ModelState.IsValid)
            {
                var bt = db.BedTypes.Find(bedType.Id);
                bt.TypeBed = bedType.TypeBed;
                bt.Description = bedType.Description;
                bt.Status = bedType.Status;
                bt.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit bed type successfully!";
                return RedirectToAction("Index");
            }
            return View(bedType);
        }


        public JsonResult DeleteBed(int id)
        {
            BedType bedType = db.BedTypes.Find(id);
            db.BedTypes.Remove(bedType);
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

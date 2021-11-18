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
    public class RoomTypesController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/RoomTypes
        public ActionResult Index()
        {
            return View(db.RoomTypes.ToList());
        }

        // GET: Administrator/RoomTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // GET: Administrator/RoomTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName,Description,Status")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                roomType.Date_Created = DateTime.Now;
                roomType.Date_Updated = DateTime.Now;
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
                TempData["success"] = "Create room type successfully!";
                return RedirectToAction("Index");
            }

            return View(roomType);
        }

        // GET: Administrator/RoomTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: Administrator/RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName,Description,Status,Date_Created,Date_Updated")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                var rt = db.RoomTypes.Find(roomType.Id);
                rt.TypeName = roomType.TypeName;
                rt.Description = roomType.Description;
                rt.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit room type successfully!";
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        // GET: Administrator/RoomTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: Administrator/RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomType roomType = db.RoomTypes.Find(id);
            db.RoomTypes.Remove(roomType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult DeleteRoomType(int id)
        {
            RoomType roomType = db.RoomTypes.Find(id);
            db.RoomTypes.Remove(roomType);
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

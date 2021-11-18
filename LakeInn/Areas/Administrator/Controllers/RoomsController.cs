using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LakeInn.Models.DataModels;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class RoomsController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.BedTypes).Include(r => r.RoomTypes);
            return View(rooms.ToList());
        }

        // GET: Administrator/Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Administrator/Rooms/Create
        public ActionResult Create()
        {
            ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed");
            ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName");
            return View();
        }

        // POST: Administrator/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MaxGuests,RoomTId,BedTId,RoomFace,Wifi,Breakfast,RoomService,AirportPickup,Description,Price,Status,Date_Created,Date_Updated")] Room room, HttpPostedFileBase fileImage)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        // Lưu ảnh theo đường dẫn
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        // Gán đường dẫn cho trường Avatar
                        room.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed");
                        ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName");
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(room);
                    }
                }
                room.Date_Created = DateTime.Now;
                room.Date_Updated = DateTime.Now;
                db.Rooms.Add(room);
                db.SaveChanges();
                TempData["success"] = "Create room successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed", room.BedTId);
            ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName", room.RoomTId);
            return View(room);
        }

        // GET: Administrator/Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed", room.BedTId);
            ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName", room.RoomTId);
            return View(room);
        }

        // POST: Administrator/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MaxGuests,RoomTId,BedTId,RoomFace,Wifi,Breakfast,RoomService,AirportPickup,Description,Price,Status,Date_Created,Date_Updated")] Room room, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var r = db.Rooms.Find(room.Id);
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        r.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed");
                        ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName");
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(room);
                    }
                }
                else
                {
                    r.Image = getAvt;
                }
                r.MaxGuests = room.MaxGuests;
                r.Name = room.Name;
                r.Price = room.Price;
                r.RoomFace = room.RoomFace;
                r.RoomService = room.RoomService;
                r.RoomTId = room.RoomTId;
                r.Status = room.Status;
                r.Wifi = room.Wifi;
                r.Breakfast = room.Breakfast;
                r.BedTId = room.BedTId;
                r.AirportPickup = room.AirportPickup;
                r.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit room successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.BedTId = new SelectList(db.BedTypes, "Id", "TypeBed", room.BedTId);
            ViewBag.RoomTId = new SelectList(db.RoomTypes, "Id", "TypeName", room.RoomTId);
            return View(room);
        }

        public JsonResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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

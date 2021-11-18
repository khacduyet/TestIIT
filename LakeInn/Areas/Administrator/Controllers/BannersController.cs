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
    public class BannersController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Banners
        public ActionResult Index()
        {
            ViewBag.Banner = db.Banners.ToList();
            ViewBag.Slides = db.Slides.ToList();
            return View();
        }

        // GET: Administrator/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Administrator/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Image,Status")] Banner banner, HttpPostedFileBase fileImage)
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
                        banner.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "File extension incorrect!");
                        return View(banner);
                    }
                    db.Banners.Add(banner);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image", "Image not null!");
                    return View(banner);
                }
            }

            return View(banner);
        }

        // GET: Administrator/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Administrator/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Image,Status")] Banner banner, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var b = db.Banners.Find(banner.Id);
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        b.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(banner);
                    }
                }
                else
                {
                    b.Image = getAvt;
                }
                b.Title = banner.Title;
                b.Status = banner.Status;
                b.Content = banner.Content;
                db.SaveChanges();
                TempData["success"] = "Edit banner successfully!";
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // Slides
        public ActionResult EditSlides(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slides slides = db.Slides.Find(id);
            if (slides == null)
            {
                return HttpNotFound();
            }
            return View(slides);
        }

        // POST: Administrator/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSlides([Bind(Include = "Id,Title,Content,Image,Status")] Banner banner, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var s = db.Slides.Find(banner.Id);
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        s.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(banner);
                    }
                }
                else
                {
                    s.Image = getAvt;
                }
                s.Title = banner.Title;
                s.Status = banner.Status;
                s.Content = banner.Content;
                db.SaveChanges();
                TempData["success"] = "Edit slides successfully!";
                return RedirectToAction("Index");
            }
            return View(banner);
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

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
    public class ArticlesController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Articles
        public ActionResult Index()
        {
            ViewBag.cmt = db.Comment_Articles;
            return View(db.Articles.ToList());
        }

        // GET: Administrator/Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Administrator/Articles/Create
        public ActionResult Create()
        {
            ViewBag.CateId = new SelectList(db.Categories,"Id","CateName");
            return View();
        }

        // POST: Administrator/Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Image,Status,CateId,Date_Created,Date_Updated, CateId")] Article article, HttpPostedFileBase fileImage)
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
                        article.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ViewBag.CateId = new SelectList(db.Categories, "Id", "CateName");
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(article);
                    }
                }
                article.Date_Created = DateTime.Now;
                article.Date_Updated = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                TempData["success"] = "Create article successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "CateName");
            return View(article);
        }

        // GET: Administrator/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "CateName", article.CateId);
            return View(article);
        }

        // POST: Administrator/Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Image,Status,CateId,Date_Created,Date_Updated, CateId")] Article article, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var a = db.Articles.Find(article.Id);
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        a.Image = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ViewBag.CateId = new SelectList(db.Categories, "Id", "CateName", article.CateId);
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(article);
                    }
                }
                else
                {
                    a.Image = getAvt;
                }
                a.CateId = article.CateId;
                a.Description = article.Description;
                a.Status = article.Status;
                a.Title = article.Title;
                a.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit article successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "CateName", article.CateId);
            return View(article);
        }

        public JsonResult DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tags(int id)
        {
            var art = db.Articles.Find(id);
            ViewBag.artId = art.Id;
            if (db.Art_Tags.Where(x => x.Art_Id == id).FirstOrDefault() != null)
            {
                var pt = db.Art_Tags.Where(x => x.Art_Id == id).FirstOrDefault();
                pt.selectedIdArray = pt.ListTag.Split(',').ToArray();
                ViewBag.Tags = new MultiSelectList(db.Tags, "Id", "Slug");
                return View(pt);
            }
            ViewBag.Tags = new MultiSelectList(db.Tags, "Id", "Slug");
            return View();
        }

        [HttpPost]
        public ActionResult Tags(int Art_Id, Art_Tags at)
        {
            var getpt = db.Art_Tags.Where(x => x.Art_Id == Art_Id).FirstOrDefault();
            at.ListTag = string.Join(",", at.selectedIdArray);
            if (getpt == null)
            {
                db.Art_Tags.Add(at);
            }
            else
            {
                getpt.ListTag = at.ListTag;
            }
            db.SaveChanges();
            @TempData["success"] = "Set tags for post success!";
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

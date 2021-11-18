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
    public class TeamsController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Teams
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: Administrator/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Administrator/Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Description,Position,Avatar,Status,Date_Created,Date_Updated")] Team team, HttpPostedFileBase fileImage)
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
                        team.Avatar = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(team);
                    }
                }
                team.Date_Created = DateTime.Now;
                team.Date_Updated = DateTime.Now;
                db.Teams.Add(team);
                db.SaveChanges();
                TempData["success"] = "Create team member successfully!";
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Administrator/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Administrator/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Description,Position,Avatar,Status,Date_Created,Date_Updated")] Team team, HttpPostedFileBase fileImage, string getAvt)
        {
            if (ModelState.IsValid)
            {
                var t = db.Teams.Find(team.Id);
                var allowedExtensions = new[] {
                ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (fileImage != null)
                {
                    var ext = Path.GetExtension(fileImage.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        fileImage.SaveAs(Path.Combine(Server.MapPath("~/Areas/Administrator/Data/Images/"), Path.GetFileName(fileImage.FileName)));
                        t.Avatar = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("Extension", "File extension incorrect!");
                        return View(team);
                    }
                }
                else
                {
                    t.Avatar = getAvt;
                }
                t.FullName = team.FullName;
                t.Description = team.Description;
                t.Position = team.Position;
                t.Status = team.Status;
                t.Date_Updated = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Edit team member successfully!";
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Administrator/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Administrator/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult DeleteTeam(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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

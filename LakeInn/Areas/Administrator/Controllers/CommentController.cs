using LakeInn.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class CommentController : BaseController
    {
        LakeInnEntities db = new LakeInnEntities();
        // GET: Administrator/Comment
        public ActionResult Index(int id)
        {
            var cmt = db.Comment_Articles.Where(x=>x.Article_Id == id);
            return View(cmt);
        }

        public ActionResult Details(int id)
        {
            var cmt = db.Comment_Articles.Find(id);
            return View(cmt);
        }

        public JsonResult ChangeComment(int id)
        {
            var cmt = db.Comment_Articles.Find(id);
            cmt.Status = !cmt.Status;
            cmt.Date_Updated = DateTime.Now;
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var cmt = db.Comment_Articles.Find(id);
            db.Comment_Articles.Remove(cmt);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
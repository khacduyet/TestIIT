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
    public class BooksController : BaseController
    {
        private LakeInnEntities db = new LakeInnEntities();

        // GET: Administrator/Books
        public ActionResult Index()
        {
            var book = db.Book.Include(b => b.RoomType);
            return View(book.ToList());
        }

        public JsonResult Details(int id)
        {
            var b = db.Book.Find(id);
            return Json(new { book = b}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoomType(int id)
        {
            var rt = db.RoomTypes.Find(id);
            return Json(new { rt = rt }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetChecked(int id)
        {
            var b = db.Book.Find(id);
            b.Status = true;
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteBook(int id)
        {
            var b = db.Book.Find(id);
            db.Book.Remove(b);
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

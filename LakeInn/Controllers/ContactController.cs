using LakeInn.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LakeInn.Controllers
{
    public class ContactController : Controller
    {
        LakeInnEntities db = new LakeInnEntities();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult sendContact(Contact con)
        {
            con.Status = false;
            con.Date_Created = DateTime.Now;
            con.Date_Updated = DateTime.Now;
            db.Contacts.Add(con);
            db.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public JsonResult sendComment(Comment_Article ca)
        {
            ca.Status = false;
            ca.Date_Created = DateTime.Now;
            ca.Date_Updated = DateTime.Now;
            db.Comment_Articles.Add(ca);
            db.SaveChanges();
            return Json(true);
        }
    }
}
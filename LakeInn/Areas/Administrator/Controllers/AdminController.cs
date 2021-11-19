using LakeInn.Areas.Administrator.Common;
using LakeInn.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class AdminController : BaseController
    {
        LakeInnEntities db = new LakeInnEntities();
        // GET: Administrator/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            var session = (User)Session["User"];
            return View(session);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(User user)
        {
            if (user.FullName == null)
            {
                ModelState.AddModelError("FullName", "Name not null!");
            }
            if (user.Email == null)
            {
                ModelState.AddModelError("Email", "Email not null!");
            }
            if (user.Email != null && user.FullName != null)
            {
                User u = db.Users.Find(user.Id);
                u.FullName = user.FullName;
                u.Email = user.Email;
                u.Phone = user.Phone;
                u.Date_Updated = DateTime.Now;
                db.SaveChanges();
                Session["User"] = u;
                Session.Remove(Constant.SESSION_USER);
                Session.Add(Constant.SESSION_USER, u);
                TempData["success"] = "Update infomation successfully!";
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangeAvatar(int Id, HttpPostedFileBase fileImage)
        {
            var user = db.Users.Find(Id);
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
                    user.Avatar = "/Areas/Administrator/Data/Images/" + fileImage.FileName;
                    db.SaveChanges();
                    Session["User"] = user;
                    TempData["success"] = "Change avatar success!";
                    return RedirectToAction("MyProfile", user);
                }
                else
                {
                    ModelState.AddModelError("Extension", "File extension incorrect!");
                    TempData["error"] = "Fail extension";
                    return RedirectToAction("MyProfile",user);
                }
            }
            TempData["error"] = "You dont choose image!";
            return RedirectToAction("MyProfile", user);
        }

        public ActionResult ChangePassword()
        {
            var session = (User)Session["User"];
            return View(session);
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPass, string Password, string ConPass)
        {
            bool chk = true;
            var session = (User)Session["User"];
            User u = db.Users.Find(session.Id);
            // encode pass & check 
            var jp = oldPass + Constant.joinPass;
            var pass = Constant.EncodePassword(jp);
            if (!pass.Equals(u.Password))
            {
                ModelState.AddModelError("OldPass", "Old password incorrect!");
                chk = false;
            }
            if (Password.Length == 0)
            {
                ModelState.AddModelError("NewPass", "Password not null!");
                
                chk = false;
            }
            if (!Password.Equals(ConPass) || ConPass.Length == 0)
            {
                ModelState.AddModelError("ConPass", "Confirm password not match!");
                chk = false;
            }
            if (chk)
            {
                var joinP = Password + Constant.joinPass;
                var NewPass = Constant.EncodePassword(joinP);
                u.Password = NewPass;
                db.SaveChanges();
                TempData["success"] = "Update password successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.old = oldPass;
            ViewBag.con = ConPass;
            ViewBag.pass = Password;
            return View();
        }
    }
}
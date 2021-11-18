using LakeInn.Areas.Administrator.Common;
using LakeInn.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class LoginController : Controller
    {
        LakeInnEntities db = new LakeInnEntities();
        // GET: Administrator/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var jp = password + Constant.joinPass;
            var pass = Constant.EncodePassword(jp);
            var user = db.Users.Where(x => x.Email == email && x.Password == pass).SingleOrDefault();
            if (user != null)
            {
                if (user.Status == false)
                {
                    TempData["error"] = "Your account not enable, please wait for the administrator to accept!";
                    return RedirectToAction("Index");
                }
                Session.Add(Constant.SESSION_USER,user);
                Session["user"] = user;
                TempData["success"] = "Hello " + user.FullName + "!";
                return RedirectToAction("Index","Admin");
            }
            TempData["error"] = "Email or password incorrect!";
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (!user.Password.Equals(ConfirmPassword))
                {
                    TempData["error"] = "Confirm password not match!";
                    return View(user);
                }
                var chUn = db.Users.Where(x => x.Email == user.Email);
                if (chUn != null)
                {
                    TempData["error"] = "Email exist, please choose another email!";
                    return View(user);
                }
                var jp = user.Password + Constant.joinPass;
                user.Password = Constant.EncodePassword(jp);
                user.Phone = "";
                user.Avatar = "/Areas/Administrator/TempLTE/dist/img/avatar.png";
                user.Status = false;
                user.Forgot = true;
                user.Date_Created = DateTime.Now;
                user.Date_Updated = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                TempData["success"] = "You have successfully registered, please wait for the administrator to accept!";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var u = db.Users.Where(x => x.Email == Email).SingleOrDefault();
            if (u != null)
            {
                u.Forgot = false;
                BuildEmailTemplate(Email);
                TempData["success"] = "Check the mailbox to confirm!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Not exist user this email!";
            return View();
        }

        public ActionResult RecoverPassword(int id)
        {
            var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult RecoverPassword(int id, string Password, string Confirm)
        {
            var data = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if (data.Forgot == false)
            {
                if (Password.Equals(Confirm))
                {
                    data.Forgot = true;
                    var jp = Password + Constant.joinPass;
                    data.Password = Constant.EncodePassword(jp);
                    db.SaveChanges();
                    TempData["success"] = "Hello " + data.FullName + "!";
                    return RedirectToAction("Index", "Admin");
                }
                TempData["error"] = "Confirm Password not match!";
                return View();
            }
            TempData["error"] = "You has change password!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(Constant.SESSION_USER);
            Session["user"] = null;
            return RedirectToAction("Index");
        }

        // Build Email
        public void BuildEmailTemplate(string Email)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Areas/Administrator/Views/Login/Template.cshtml"));
            var regInfo = db.Users.Where(x => x.Email == Email).FirstOrDefault();
            var url = "http://localhost:53184/" + Url.Action("RecoverPassword", "Login") + "/" + regInfo.Id;
            body = body.Replace("@ViewBag.ConfirmLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your account is successfully created!", body, regInfo.Email);
        }

        public static void BuildEmailTemplate(string subText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "vsms.kd.ht@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("vsms.kd.ht@gmail.com", "123123Aa");
            try
            {
                client.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using LakeInn.Models.DataModels;
using LakeInn.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace LakeInn.Controllers
{
    public class HomeController : Controller
    {
        LakeInnEntities db = new LakeInnEntities();
        public ActionResult Index()
        {
            ViewBag.Articles = db.Articles.Take(4);
            ViewBag.Team = db.Teams.OrderByDescending(x => Guid.NewGuid()).Take(2);
            ViewBag.RT = new SelectList(db.RoomTypes, "Id", "TypeName");
            ViewBag.Slides = db.Slides.Where(y=>y.Status);
            ViewBag.Banner_I1 = db.Banners.Where(x => x.Id == 1).SingleOrDefault();
            ViewBag.Banner_I2 = db.Banners.Where(x => x.Id == 2).SingleOrDefault();
            ViewBag.About_I1 = db.Banners.Where(x => x.Id == 3).SingleOrDefault().Image;
            ViewBag.About_I2 = db.Banners.Where(x => x.Id == 4).SingleOrDefault().Image;
            ViewBag.About_I3 = db.Banners.Where(x => x.Id == 5).SingleOrDefault().Image;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.TitleSmall = "About";
            ViewBag.Team = db.Teams.OrderByDescending(x => Guid.NewGuid()).Take(2);
            ViewBag.Banner_I2 = db.Banners.Where(x => x.Id == 2).SingleOrDefault();
            ViewBag.About_I1 = db.Banners.Where(x => x.Id == 3).SingleOrDefault().Image;
            ViewBag.About_I2 = db.Banners.Where(x => x.Id == 4).SingleOrDefault().Image;
            ViewBag.About_I3 = db.Banners.Where(x => x.Id == 5).SingleOrDefault().Image;
            return View();
        }

        public ActionResult OurTeam()
        {
            ViewBag.Banner_I1 = db.Banners.Where(x => x.Id == 1).SingleOrDefault();
            ViewBag.Team = db.Teams.OrderByDescending(x => Guid.NewGuid()).Take(2);
            ViewBag.TitleSmall = "Team";
            return View();
        }

        public ActionResult RoomRate()
        {
            ViewBag.TitleSmall = "Rooms & Rate";
            return View(db.Rooms);
        }

        public ActionResult DetailRoom(int id)
        {
            ViewBag.TitleSmall = "Rooms & Rate";
            ViewBag.rs = (from r in db.Rooms
                          join rt in db.RoomTypes on r.RoomTId equals rt.Id
                          join bt in db.BedTypes on r.BedTId equals bt.Id
                          where r.Id == id
                          select new RoomViewModel
                          {
                              Id = r.Id,
                              BedTId = bt.Id,
                              AirportPickup = r.AirportPickup,
                              Breakfast = r.Breakfast,
                              Description = r.Description,
                              Image = r.Image,
                              MaxGuests = r.MaxGuests,
                              Name = r.Name,
                              Price = r.Price,
                              RoomFace = r.RoomFace,
                              RoomService = r.RoomService,
                              RoomTId = rt.Id,
                              Status = r.Status,
                              TypeBed = bt.TypeBed,
                              TypeName = rt.TypeName,
                              Wifi = r.Wifi
                          }).SingleOrDefault();
            ViewBag.Tags = db.Tags.OrderByDescending(x => Guid.NewGuid()).Take(10);
            ViewBag.Cat = db.Categories.OrderByDescending(x => Guid.NewGuid()).Take(10);
            ViewBag.Art = db.Articles.Take(3).ToList();
            return View();
        }

        public ActionResult Article(int? idCate, int? idTag, int? page)
        {
            page = page ?? 1;
            int pageSize = 1;
            IEnumerable<Article> data;
            if (idCate != null)
            {
                data = db.Articles.Where(x => x.CateId == idCate);
            } else if (idTag != null)
            {
                List<Article> lst = new List<Article>();
                foreach (var item in db.Art_Tags)
                {
                    foreach (var arr in item.ListTag.Split(',').ToArray())
                    {
                        if (arr.Equals(idTag.ToString()))
                        {
                            lst.Add(db.Articles.Find(item.Art_Id));
                        }
                    }
                }
                data = lst.AsEnumerable();
            }
            else
            {
                data = db.Articles;
            }
            ViewBag.TitleSmall = "Article";
            ViewBag.comment = db.Comment_Articles.Where(z=>z.Status);
            return View(data.OrderBy(x=>x.Id).ToPagedList(page.Value,pageSize));
        }

        public ActionResult DetailArticle(int id)
        {
            var dt = db.Articles.Find(id);
            ViewBag.TitleSmall = dt.Title;
            ViewBag.Comment = db.Comment_Articles.Where(x => x.Article_Id == id && x.Status == true).OrderByDescending(y=>y.Date_Created);
            // Get tags relation 
            var getpt = db.Art_Tags.Where(x => x.Art_Id == dt.Id).FirstOrDefault();
            getpt.selectedIdArray = getpt.ListTag.Split(',').ToArray();
            List<Tags> t = new List<Tags>();
            foreach (var item in getpt.selectedIdArray)
            {
                foreach (var tag in db.Tags)
                {
                    if (item.Equals(tag.Id.ToString()))
                    {
                        t.Add(tag);
                    }
                }
            }
            ViewBag.lstTag = t;
            // End get tags
            ViewBag.Tags = db.Tags.OrderByDescending(x => Guid.NewGuid()).Take(10);
            ViewBag.Cat = db.Categories.OrderByDescending(x => Guid.NewGuid()).Take(10);
            ViewBag.Art = db.Articles.Take(3).ToList();
            return View(dt);
        }

        public ActionResult Galery(int? page)
        {
            page = page ?? 1;
            int pageSize = 1;
            var art = db.Articles.OrderBy(x=>x.Id);
            ViewBag.TitleSmall = "Grid";
            return View(art.ToPagedList(page.Value,pageSize));
        }

        public ActionResult Contact()
        {
            ViewBag.TitleSmall = "Contacts";
            return View();
        }

        [HttpPost]
        public JsonResult PostBook(Book book)
        {
            book.Status = false;
            book.Date_Created = DateTime.Now;
            book.Date_Updated = DateTime.Now;
            db.Book.Add(book);
            db.SaveChanges();
            return Json(true);
        }
    }
}
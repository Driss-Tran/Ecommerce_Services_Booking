using FinalCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharp.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        BookingEntities _db = new BookingEntities(); 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsDetail(String meta)
        {
            var v = from t in _db.News
                    where t.meta == meta
                    select t;
            ViewBag.type = "tin-tuc";
            return View(v.FirstOrDefault());
        }

        public ActionResult RecentNews()
        {
            var v = (from t in _db.News
                     orderby t.datebegin descending
                    select t).Take(3);
            return PartialView(v.ToList()); 
        }
    }
}
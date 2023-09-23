using FinalCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharp.Controllers
{
    public class HomeController : Controller
    {
        BookingEntities _db = new BookingEntities();
        public ActionResult Index(String meta)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        

        public ActionResult Contact() {
            return View(); 
        }



        public ActionResult getSlider()
        {
            var v = from t in _db.slider_home select t;
            return PartialView(v.ToList());
        }



        public ActionResult getNews() {
            ViewBag.meta = "tin-tuc";
            var v = from t in _db.News select t;
            return PartialView(v.ToList());

        }



        public ActionResult getCategory()
        {
            
            ViewBag.meta = "dich-vu";
            
            var v = from t in _db.Categories
                    where t.hide == true
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getTourCategory()
        {
            ViewBag.meta = "dich-vu";

            var v = from t in _db.Categories
                    where t.hide == true && (t.id == 1 || t.id == 2)
                    select t;
            return PartialView(v.ToList());

        }


        public ActionResult getHotelCategory()
        {
            ViewBag.meta = "dich-vu";

            var v = from t in _db.Categories
                    where t.hide == true && (t.id == 3 || t.id == 4)
                    select t;
            return PartialView(v.ToList());

        }


        public ActionResult getServices(long id, string metaTitle)
        {
            ViewBag.meta = "dich-vu";
            var v = (from t in _db.Products
                     where t.category_id == id && t.hide == true
                     select t).Take(6);
            return PartialView(v.ToList());
        }


        public ActionResult getDetailCategoryServices(long id, string metaTitle)
        {
            ViewBag.meta = metaTitle;
            var v = from t in _db.Products
                    where t.category_id == id && t.hide == true
                    select t;
            return PartialView(v.ToList());
        }
    }
}
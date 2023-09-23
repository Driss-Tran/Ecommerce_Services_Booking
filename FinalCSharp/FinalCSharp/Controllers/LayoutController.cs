using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalCSharp.Models;
namespace FinalCSharp.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Footer
        BookingEntities _db = new BookingEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getMenu(String metaTile)
        {

            var v = from t in _db.Menus
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getMenuFooter()
        {
            var v = from t in _db.Menus
                    where t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getFooter()
        {
            var v = from t in _db.Footers select t;
            return PartialView(v.ToList()); 
        }

        public ActionResult getInfo()
        {
            var v = from t in _db.Footers select t;
            return PartialView(v.ToList());
        }

        


    }
}
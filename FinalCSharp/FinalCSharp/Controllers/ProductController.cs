using FinalCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FinalCSharp.Controllers
{
    public class ProductController : Controller
    {
        BookingEntities _db = new BookingEntities();
        // GET: Product
        public ActionResult Index(String meta)
        {
            var v = from t in _db.Categories
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult Hotel()
        {
            return View();
        }

        public ActionResult Tour()
        {
            return View();
        }


        public ActionResult Detail(String meta)
        {
         
            var v = from t in _db.Products
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult getOtherService(int category_id)
        {
            var sliderContent = (from t in _db.Products
                    where t.category_id == category_id
                    orderby t.review descending 
                    select t
                    ).Take(6).ToList(); 

            
            List<List<Product>> sliders = new List<List<Product>>();

            for(int i = 0; i < sliderContent.Count; i+=3)
            {
                List<Product> slider  = new List<Product>();
                slider.Add(sliderContent[i]);
                if(i+1 < sliderContent.Count)
                {
                    slider.Add(sliderContent[i+1]);
                }
                if(i+2 < sliderContent.Count)
                {
                    slider.Add(sliderContent[i + 2]);
                }
                sliders.Add(slider);

            }
            ViewBag.meta = "dich-vu";
            //ViewBag.sliders = sliders;
            return PartialView(sliders);
        }
 
        
    }
}










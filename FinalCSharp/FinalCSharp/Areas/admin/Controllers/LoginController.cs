using FinalCSharp.Areas.admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharp.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(AdminInfo user)
        {
            Session["admin"] = user.username;
            if(user.username.ToString().Equals("admin") && user.password.ToString().Equals("admin"))
            {
                return Redirect("/admin");
            }
            return RedirectToAction("Index");
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FinalCSharp.Models;

namespace FinalCSharp.Controllers
{
    public class LoginController : Controller
    {
        BookingEntities _db = new BookingEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(User user)
        {
            var checkPassword = (from p in _db.Users
                           where user.email == p.email
                           select p.password).FirstOrDefault();
            string decryptPass = DecryptPassword(checkPassword);

            var check = (from t in _db.Users
                        where user.email == t.email && user.password == decryptPass
                         select t).FirstOrDefault();
            if (check==null)
            {
                user.LoginErrorMessage = "Email hoặc mật khẩu của bạn chưa đúng. Vui lòng thử lại!";
                return View("Index",user);
            }
            else
            {
                Session["id"] = check.id;
                Session["email"] = check.email;
                Session["fullName"] = check.fullName;
                Session["phone"] = check.phone;
                Session["address"] = check.address;
                return RedirectToAction("Index","Home");
            } 
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.email == user.email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    user.password = EncryptPassword(user.password);
                    user.confirmpassword = EncryptPassword(user.password);
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    // Chuyen huong sang login;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã có người sử dụng";
                    return View();
                }
            }
            return View();
        }

        public string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
            
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
       
    }
}
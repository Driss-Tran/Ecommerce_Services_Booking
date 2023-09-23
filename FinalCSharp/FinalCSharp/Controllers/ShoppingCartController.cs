using FinalCSharp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace FinalCSharp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private BookingEntities _db = new BookingEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            
            if (cart != null)
            {
                return View(cart.Items);
            }
            return View();
        }


        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];

            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            
            return View();
        }

        public ActionResult Partial_Item_Payment()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }


        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult Update(int id,int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }


        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Success = true, Count = cart.Items.Count },JsonRequestBehavior.AllowGet);

            }
            return Json(new {Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }


        public ActionResult Pay_By_ATM(Order order)
        {
            
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                Order order = new Order();
                order.customer_name = req.customer_name;
                order.phone = req.phone;
                order.address = req.address;
                order.email = req.email;
                cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                {
                    product_id = x.ProductId,
                    quantity = x.Quantity,
                    price = x.Price,

                }));
                cart.Items.ForEach(x => order.quantity = x.Quantity);
                order.total_amount = cart.Items.Sum(x => (x.Quantity) * Convert.ToInt32(x.Price)).ToString();
                order.type_payment= req.type_payment;
                order.created_by = req.phone;
                order.created_date = DateTime.Now;
                Random rd = new Random();
                order.code = "DV" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9)+ rd.Next(0, 9);
                try
                {
                    _db.Orders.Add(order);
                    _db.SaveChanges();
                    cart.ClearCart();
                    if (req.type_payment == 1)
                    {
                        return RedirectToAction("CheckOutSuccess");
                    }
                    else
                    {
                        return RedirectToAction("Pay_By_ATM",order);
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    return Redirect("/");
                }
            }
                
            return Json(code);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity) {
            var code = new { Success = false, msg = "", code = -1 ,Count= 0};
            BookingEntities _db = new BookingEntities();
            var checkProduct = _db.Products.FirstOrDefault(x => x.id == id);
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.id,
                    ProductName = checkProduct.name,
                    CategoryId = (int)checkProduct.category_id,
                    Quantity = quantity,
                    Meta = checkProduct.meta

                };
                if (checkProduct.img_link != null)
                {
                    item.ProductImg = checkProduct.img_link;
                }
                var price = Convert.ToInt32(checkProduct.price.Replace(".", ""));
                if (price > 0)
                {
                    item.Price = price.ToString();
                }
                item.TotalPrice = (Convert.ToInt32(item.Quantity) * Convert.ToInt32(item.Price)).ToString();
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct  = cart.Items.FirstOrDefault(x=>x.ProductId== id);
                if(checkProduct!= null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = -1, Count = cart.Items.Count };
                }
            }
            return Json(code);

        }


        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new {Success = true});
            }
            return Json(new { Success = false }); 
        }

    }
}
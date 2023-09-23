using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCSharp.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart() {
            this.Items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int Quantity)
        {
            var checkExist = Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
            if (checkExist != null)
            {
                checkExist.Quantity += Quantity;
                checkExist.TotalPrice = (Convert.ToInt32(checkExist.Price) * Convert.ToInt32(checkExist.Quantity)).ToString();


            }
            else
            {
                Items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExist = Items.SingleOrDefault(x=>x.ProductId == id);
            if(checkExist != null)
            {
                Items.Remove(checkExist);
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkExist = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExist != null)
            {
                checkExist.Quantity = quantity;
                checkExist.TotalPrice = (Convert.ToInt32(checkExist.Price) * Convert.ToInt32(checkExist.Quantity)).ToString();
            }
        }


        public decimal GetTotalPrice()
        {
            return Items.Sum(x => Convert.ToInt32(x.TotalPrice));
        }

        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }

        public void ClearCart()
        {
            Items.Clear();
        }


        
    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Meta { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }   
        public string TotalPrice { get; set; }
    }

}
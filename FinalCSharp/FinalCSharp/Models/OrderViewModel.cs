using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCSharp.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string customer_name { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string phone { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string address { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string email { get; set; }
        public Nullable<int> type_payment { get; set; }
    }
}
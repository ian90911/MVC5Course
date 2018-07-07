﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class ProductPartial
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="名稱必填哦")]
        [StringLength(10,ErrorMessage ="商品名稱不可超過10個字")]
        public string ProductName { get; set; }


        public Nullable<decimal> Price { get; set; }

        [Required]
        public Nullable<bool> Active { get; set; }

        [Required]
        public Nullable<decimal> Stock { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MOKANZ.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        [Display(Name ="Description")]
        public string ProductDescription { get; set; }
        public string Price { get; set; }
        public string Retailer { get; set; }

        [Display(Name = "Units in Stock")]
        public int? UnitsInStock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Quantity cannot be less than 1.")]
        public int Quantity { get; set; }
        public string Category { get; set; }
        public byte[] Image { get { return binary; } set { binary = value; } }

        public string SImage
        {
            get
            {
                return string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(binary));
            }
        }

        private byte[] binary;
        }
    }

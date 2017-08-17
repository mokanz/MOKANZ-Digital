using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalX.Models
{
    public class OrderItem
    {
        //public ProductOrders()
        //{
        //}

        public Product product { get; set; }
        public int numberOfOrders { get; set; }
    }
}
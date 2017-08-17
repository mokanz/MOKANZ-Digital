using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalX.Models
{
    public class CartViewModel
    {
        public IEnumerable<OrderItemModel> OrderDetails { get; set; }
        public string SubTotal { get; set; }
    }
}
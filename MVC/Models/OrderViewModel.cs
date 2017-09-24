using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MOKANZ.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Order Complete?")]
        public string Complete { get; set; }
        [DisplayName("Back Order?")]
        public string IsBackOrder { get; set; }

        public IEnumerable<OrderItemModel> OrderDetails { get; set; }
        public string SubTotal { get; set; }
    }
}
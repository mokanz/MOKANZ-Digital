using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalX.Models
{
    public class CheckOutViewModel
    {
        public IEnumerable<Address> Addresses { get; set; }
        public Address Address { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
     }
}
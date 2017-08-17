using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOKANZ.Models
{

    //model for each item in the order (from OrderDetail table)
    [JsonObject]
    public class OrderItemModel
    {
        public string Product { get; set; }

        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public int DetailID { get; set; }
        public string Total { get; set; }
        public string Price { get; set; }
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

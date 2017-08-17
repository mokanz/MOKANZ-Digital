using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalX.Models
{
    [JsonObject]
    public class OrderItemModel
    {
        public string Product { get; set; }
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

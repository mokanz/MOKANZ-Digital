using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalX.Models
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public string CategoryID { get; set; }
        public string ParentCategory { get; set; }
    }
}
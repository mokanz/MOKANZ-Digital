using DigitalX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigitalX.Controllers
{
    public class CategoriesAPIController : ApiController
    {
        DigitalXDB db = new DigitalXDB();


        ////get categories and their parents to construct the Categories dropdown 
        //public IEnumerable<CategoryModel> GetCategories()
        //{
        //    var categories = db.ProductCategories.Where(a => a.ParentCategory != 0).AsEnumerable()
        //        .Join(db.ProductCategories.AsEnumerable(),
        //        pc=>pc.ParentCategory,
        //        c=>c.CategoryID,
        //        (pc, c) => new CategoryModel
        //        {
        //            CategoryID = pc.CategoryID.ToString(),
        //            CategoryName = pc.Category,
        //            ParentCategory = c.Category
        //        }
        //        );
        //    return categories;
        //}
    }
}

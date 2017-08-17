using MOKANZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOKANZ.Controllers
{
    public class HomeAPIController : ApiController
    {
        MOKANZEntities1 db = new MOKANZEntities1();


        //return top 5 most purchased products
       [HttpGet]
        public  IHttpActionResult GetTop5Products()
        {  
            var orderdetails = db.OrderDetails;

            var query =
  (from item in db.OrderDetails.ToList()
   group item.Quantity by item.Product into g

   orderby g.Sum() descending
   select new ProductModel {
       ProductID = g.Key.ProductID,
       ProductName = g.Key.ProductName,
       Price = g.Key.Price.ToString("C"),
       Image = g.Key.Picture 

   }).Take(5);


            return Ok(query);














        }
    }
}

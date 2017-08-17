using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MOKANZ.Models;
using MOKANZ.Repositories;
using System.Threading.Tasks;
using System.Globalization;

namespace MOKANZ.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
       
        OrdersRepository repository = new OrdersRepository();

       
        public async Task<ActionResult> MyOrders()
        {

            int customerid = int.Parse(Session["CustomerID"].ToString());
            var orders = await repository.GetOrders(customerid); //customerid

            foreach (OrderViewModel order in orders)
            {
                order.OrderDetails = await repository.GetOrderItems(order.OrderID);
                order.SubTotal = order.OrderDetails.Sum(v => double.Parse(v.Total, NumberStyles.Currency))
                .ToString("C"); 
            }

            return View(orders.OrderByDescending(d=> d.OrderDate));
        }

        
        public async Task<ActionResult> Cart()
        {

            OrderViewModel cart = new OrderViewModel();
            int customerid = int.Parse(Session["CustomerID"].ToString());
            cart.OrderDetails = await repository.GetCart(customerid);  //customerid

            if (cart.OrderDetails.Count() > 0) { 
            Session["CartID"] = cart.OrderDetails.First().OrderID;
            }
            

            cart.SubTotal = cart.OrderDetails.Sum(v => double.Parse(v.Total, NumberStyles.Currency))
                .ToString("C");
            return View(cart);

        }


        [HttpPost]
        public async Task<ActionResult> CheckOut(OrderViewModel model)
        {
            if (!ModelState.IsValid) { return View(model);  }


            //SAVE ORDER

            MOKANZEntities1 db = new MOKANZEntities1();

            try {
                var order = db.Orders.Single(a => a.OrderID == model.OrderID);

                order.Complete = true;
                order.OrderDate = DateTime.Now;
                await db.SaveChangesAsync();

                //also need update the Product table and change the number of units in stock...

                return RedirectToAction("MyOrders");
            }
            catch (Exception e) { return View("There was a problem saving your order. Details: "+e.Message); }

            
        }


        public async Task<ActionResult> CheckOut()
        {

            MOKANZEntities1 db = new MOKANZEntities1();
            int cartid = Convert.ToInt32(Session["CartID"].ToString());
            var v = db.Orders.Single(a => a.OrderID == cartid); //cartID

            OrderViewModel o = new OrderViewModel();

            o.OrderID = v.OrderID;
            o.OrderDetails = v.OrderDetails.Select(a => new OrderItemModel
            {
                Product = a.Product.ProductName,
                Quantity = a.Quantity,
                Total = (a.Quantity * a.Product.Price).ToString()
                
            });


            o.SubTotal = o.OrderDetails.Sum(e => double.Parse(e.Total, NumberStyles.Currency))
               .ToString("C");

            int customerid = int.Parse(Session["CustomerID"].ToString());
            Address Address = await repository.GetCustomerAddresses(customerid); //customerid
            ViewBag.address = Address;

            return View(o);
        }
        
        }
}
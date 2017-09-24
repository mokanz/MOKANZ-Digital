using MOKANZ.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MOKANZ.Controllers
{
    
    public class CartAPIController : ApiController
    {
        MOKANZEntities1 db = new MOKANZEntities1();

        [HttpPost]
        [ActionName("AddToCart")]
        public string Add(OrderDetail orderdetail, int id) //id is customerid
        {
            
            try
            {
                Order cart = Cart(id);

                //check if item already in the cart, if so only increment the quantity number
                var item = cart.OrderDetails
                .Where(a => a.ProductID == orderdetail.ProductID).SingleOrDefault();

                if (item != null) //item already exists in the cart, so only increment the quatity 
                {
                    item.Quantity += orderdetail.Quantity;
                    db.SaveChangesAsync();
                }

                else { //item is not in the cart, so add it.

                    cart.OrderDetails.Add(orderdetail);
                    db.SaveChangesAsync();
                }
                
                return "success"; //productname + " has been added to your cart.";
            }

            catch (Exception e)
            { return e.Message; }
        }


        [HttpGet]
        [ActionName("GetShoppingCart")]
        public IEnumerable<OrderItemModel> ShoppingCart(int id) //id is customerid
        {
            try
            {
                Order cart = Cart(id);
                
                
                return OrderItems(cart.OrderID);
            }

            catch (Exception)
            { return null; }
        }

        //returns a list of items (products) in an order
        [HttpGet]
        [ActionName("GetOrderItems")]
        public IEnumerable<OrderItemModel> OrderItems(int id) //OrderID
        {
            
            try
            {
                var orderitems = db.OrderDetails.ToList()
                .Where(a => a.OrderID == id)  

                .Select(a => new OrderItemModel
                {
                    OrderID = a.OrderID,
                    DetailID = a.DetailID,
                    Image = a.Product.Picture,
                    Product = a.Product.ProductName,
                    Quantity = a.Quantity,
                    //     Packaged = a.Packaged,
                    Price = a.Product.Price.ToString("C"),
                    Total = (a.Quantity * a.Product.Price).ToString("C")

                });
                
                return orderitems;
            }
            catch (Exception e) { return null; }
        }



        //  public 

            //gets a single order item
        [HttpGet]
        [ActionName("GetOrderDetail")]
        public OrderDetail OrderDetail(int id)
        {

            var order = db.OrderDetails.Single(o => o.DetailID == id);
                //.Where(o => o.DetailID == id).SingleOrDefault();
            return order;
        }

        //[HttpGet]
        //[ActionName("GetOrder")]
        //public Order Order(int id)
        //{

        //    var order = db.Orders.Single(o => o.OrderID == id);
        //        //.Where(o => o.OrderID == id).SingleOrDefault();
        //    return order;
        //}

        [HttpGet]
        [ActionName("GetOrders")]
        public IEnumerable<OrderViewModel> Orders(int id) //id is customerid
        {
          var orders =  db.Orders.ToList()
                .Where(a => a.CustomerID == id)
                .Select(a => new OrderViewModel
                {
                    OrderID = a.OrderID,
                    Complete = a.Complete ? "Yes": "No",
                    IsBackOrder = a.IsBackOrder ? "Yes" : "No",
                    OrderDate = a.OrderDate
                });

            return orders;
        }



        //Shopping cart is created as an order and in not complete unless check out
        //any item added to the Shopping Cart is added to this Order
        [HttpGet]
        private Order Cart(int id) //id is customerid
        {
            var cart = db.Orders.Where(a => a.Complete == false &&
            a.CustomerID == id &&
            a.IsBackOrder == false
            ).SingleOrDefault();

            //no order - create new 
            if (cart == null)
            {
                cart = Create(id, false);
            }
            return cart;
        }

        [HttpGet]
        [ActionName("CreateOrder")] //create new order - also used when creating backorder
        private Order Create(int id, bool isBackOrder)
        {
            Order o = db.Orders.Add(new Order()
            { IsBackOrder = isBackOrder,
                CustomerID = id,
                Complete = false,
                OrderDate = DateTime.Now,
                AddressID = db.Customers.Single(a => a.CustomerID == id).Addresses.First().AddressID
        });
            db.SaveChanges();
            return o;
        }

        [HttpGet]
        [ActionName("DeleteItem")]
        public async Task<string> AjaxDelete(int id)
        {
            try
            {
                OrderDetail item = db.OrderDetails.Single(a => a.DetailID == id);
                int orderID = item.OrderID;

                db.OrderDetails.Remove(item);
                await db.SaveChangesAsync();

                //return updated cart subtotal
                return OrderSubTotal(orderID); 
            }
            catch (Exception e)
            {
                return "There was a problem deleting this item from your shopping cart. Details: " + e.Message;
            }
        }



        [HttpPost]
        [ActionName("UpdateItem")]
        public async Task<string> AjaxUpdate(JObject jsonData)
        {
            dynamic json = jsonData;
            int quantity = json.quantity;
            int id = json.id;

            try
            {
                OrderDetail item = db.OrderDetails.Single(a => a.DetailID == id);

                int orderID = item.OrderID;

                item.Quantity = quantity;
                await db.SaveChangesAsync();
                
                
                //return updated cart subtotal
                return OrderSubTotal(orderID);
            }
            catch (Exception e)
            {
                return "There was a problem updating this item in your shopping cart. Details: " + e.Message;
            }
        }

       



        [NonAction]
        private string OrderSubTotal(int OrderID)
        {
            var items = OrderItems(OrderID);
            return items.Sum(v => double.Parse(v.Total, NumberStyles.Currency))
                .ToString("C");

        }





        [HttpGet]
        public Customer GetCustomer(int id)
        {
            //Customer customer = 
            return db.Customers.Single(a => a.CustomerID == id);



        }



        [HttpGet]
        [ActionName("GetAddresses")]
        public Address Addresses(int id) //customerid
        {
            var addresses = db.Customers.Single(a => a.CustomerID == id).Addresses.First();


            return addresses;
        }



    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MOKANZ.Models;

namespace MOKANZ.Repositories
{
    public class OrdersRepository
    {
        public HttpClient apiclient = new HttpClient
        {
            BaseAddress = new Uri(
                APIUri.BaseAddress
                
                )
        };



        public async Task<IEnumerable<OrderViewModel>> GetOrders(int id)
        {
            var reply = await apiclient.GetAsync(APIUri.CartAPI + "getorders/"+ id); //"api/accountapi/
            return reply.Content.ReadAsAsync<IEnumerable<OrderViewModel>>().Result;
        }


        public async Task<IEnumerable<OrderItemModel>> GetCart(int id)
        {
            var reply = await apiclient.GetAsync(APIUri.CartAPI + "getshoppingcart/" + id); //customerid  //"api/accountapi/
            return reply.Content.ReadAsAsync<IEnumerable<OrderItemModel>>().Result;
        }


        public async Task<IEnumerable<OrderItemModel>> GetOrderItems(int id) //orderID
        {
            var reply = await apiclient.GetAsync(APIUri.CartAPI + "getorderitems/" + id); //"api/accountapi/
            return reply.Content.ReadAsAsync<IEnumerable<OrderItemModel>>().Result;
        }


        public async Task<Customer> GetCustomer(int id) //customerid
        {
            var reply = await apiclient.GetAsync(APIUri.CartAPI + "getcustomer/" + id); //"api/accountapi/
            return reply.Content.ReadAsAsync<Customer>().Result;
        }


        public async Task<Address> GetCustomerAddresses(int id) //customerid
        {
            var reply = await apiclient.GetAsync(APIUri.CartAPI + "getaddresses/" + id); //"api/accountapi/
            return reply.Content.ReadAsAsync<Address>().Result;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using MOKANZ.Models;
using System.Threading.Tasks;
using MvcSiteMapProvider;

namespace MOKANZ.Repositories
{
    public class ProductsRepository
    {
        public HttpClient apiclient = new HttpClient
        {
            BaseAddress = new Uri(
                APIUri.BaseAddress
                )
        };

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            //all products
            HttpResponseMessage reply = await apiclient.GetAsync(APIUri.ProductsAPI + "getproducts");  //api/productsapi/getproducts
            return reply.Content.ReadAsAsync<IEnumerable<ProductModel>>().Result;

        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            //categories drop down
            HttpResponseMessage catsreply = await apiclient.GetAsync(APIUri.ProductsAPI + "getcategories"); //api/productsapi/getcategories
            return catsreply.Content.ReadAsAsync<IEnumerable<CategoryModel>>().Result; 
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            HttpResponseMessage reply = await apiclient.GetAsync(APIUri.ProductsAPI + "getproduct/" + id);
            return reply.Content.ReadAsAsync<ProductModel>().Result;
        }


        public async Task<IEnumerable<DynamicNode>> GetBreadcrumbNodes()
        {
            HttpResponseMessage reply = apiclient.GetAsync(APIUri.ProductsAPI + "getbreadcrumbnodes").GetAwaiter().GetResult();
            return reply.Content.ReadAsAsync<IEnumerable<DynamicNode>>().GetAwaiter().GetResult();
        }
    }
}
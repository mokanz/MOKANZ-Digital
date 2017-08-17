using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MOKANZ.Models;
using System.Configuration;
using MOKANZ.Repositories;

namespace MOKANZ.Controllers
{
    public class HomeController : Controller
    {

        public HttpClient apiclient = new HttpClient
        {
            BaseAddress = new Uri(
                APIUri.BaseAddress
                //ConfigurationManager.AppSettings["BaseAddress"].ToString()
                )
        };
        
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";

            HttpResponseMessage reply = await apiclient.GetAsync("api/homeapi");
            object result =  reply.Content.ReadAsAsync<List<ProductModel>>().Result;
            return View(result);
        }
    }
}

using MOKANZ.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MOKANZ.Repositories;

namespace MOKANZ.Controllers
{
    public class ProductsController : Controller
    {
        
        ProductsRepository repository = new ProductsRepository();


        // GET: Products
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Products";

            //all products
            var result = await repository.GetProducts();

            //categories drop down
            ViewBag.ddcategory = new SelectList(await repository.GetCategories(), "categoryid", "categoryname", "ParentCategory", new CategoryModel { });

            return View(result);
        }

        // GET: Products/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            //if (!string.IsNullOrEmpty(Session["CustomerID"] as string))
            //{
            //    ViewBag.cusId = Session["CustomerID"].ToString();
            //}

            var result = await repository.GetProduct(id);
            return View(result);
        }
    }
}

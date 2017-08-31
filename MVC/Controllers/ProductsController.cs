using MOKANZ.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using MOKANZ.Repositories;
using MvcSiteMapProvider;

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

        // GET: Products/Details/x
        [MvcSiteMapNode(Title = "Details (Code)", ParentKey = "Products", Key = "Details2")]
        [HttpGet]
        [MvcSiteMapNodeAttribute(Title = "Details", ParentKey = "Products", DynamicNodeProvider = "MOKANZ.Code.DetailsDynamicNodeProvider, MOKANZ")]
        public async Task<ActionResult> Details(int id)
        {

            ViewBag.id = id;

            var result = await repository.GetProduct(id);
            return View(result);
        }
    }
}

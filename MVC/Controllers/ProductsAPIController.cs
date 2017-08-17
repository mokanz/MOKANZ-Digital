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
    public class ProductsAPIController : ApiController
    {
        MOKANZEntities1 db = new MOKANZEntities1();


        //get all products
        public IEnumerable<ProductModel> GetProducts()
        {

            var products = db.Products.ToList()
                .Select(a => new ProductModel {
                    ProductName = a.ProductName,
                    ProductID = a.ProductID,
                    Image = a.Picture, 
                    UnitsInStock = a.UnitsInStock,
                    Price = a.Price.ToString("C")
                });

            return products;
        }

        //        get a product - Details page
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product a = await db.Products.FindAsync(id);
            if (a == null)
            {
                return NotFound();
            }

            return Ok(new ProductModel {
                ProductName = a.ProductName,
                ProductDescription = a.ProductDescription,
                ProductID = a.ProductID,
                Retailer = a.Retailer.Retailer1,
                Image = a.Picture, 
                UnitsInStock = a.UnitsInStock,
                Quantity = 1,
                Category = a.ProductCategory.Category,
                Price = a.Price.ToString("C")
            });

        }

        //get filtered results products
        public IEnumerable<ProductModel> GetProducts(int id)
        {

            var products = db.Products.ToList()
                .Where(b => b.CategoryID == id)
                .Select(a => new ProductModel
                {
                    ProductName = a.ProductName,
                    ProductID = a.ProductID,
                    Image = a.Picture, 
                    UnitsInStock = a.UnitsInStock,
                    Price = a.Price.ToString("C")
                });

            return products;
        }


        //get categories and their parents to construct the Categories dropdown 
        public IEnumerable<CategoryModel> GetCategories()
        {
            var categories = db.ProductCategories.Where(a => a.ParentCategory != 0).AsEnumerable()
                .Join(db.ProductCategories.AsEnumerable(),
                pc => pc.ParentCategory,
                c => c.CategoryID,
                (pc, c) => new CategoryModel
                {
                    CategoryID = pc.CategoryID.ToString(),
                    CategoryName = pc.Category,
                    ParentCategory = c.Category
                }
                );
            return categories;
        }


    }
}

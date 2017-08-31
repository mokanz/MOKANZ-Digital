using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using MOKANZ.Repositories;
using MOKANZ.Models;

namespace MOKANZ.Code
{
    public class DetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            //ProductsRepository repository = new ProductsRepository();

            //IEnumerable<DynamicNode> dynamicNode = Enumerable.Empty<DynamicNode>();
            //dynamicNode = repository.GetBreadcrumbNodes().Result as IEnumerable<DynamicNode>;

            // return dynamicNode;

            MOKANZEntities1 db = new MOKANZEntities1();
            foreach (var product in db.Products.Include("ProductCategory"))
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = product.ProductCategory.Category; //product.ProductName;
                dynamicNode.ParentKey = "Products"; //"Cat_" + product.ProductCategory.Category;
                dynamicNode.RouteValues.Add("id", product.ProductID);

                yield return dynamicNode;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using MOKANZ.Repositories;
using MOKANZ.Models;

namespace MOKANZ.Code
{
    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        
        ProductsRepository repository = new ProductsRepository();
        
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            IEnumerable<DynamicNode> result = repository.GetBreadcrumbNodes().Result;
            return result;
            
        }
    }
}
using Best.Service.CategoryServices;
using Best.Service.GaleryServices;
using MvcSiteMapProvider;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Best.Web.Infrastructure.Extensions
{
    public class CategoryDetailDynamicNodeProvider : DynamicNodeProviderBase
    {
        public ICategoryService _categoryService
        {
            get
            {
                return DependencyResolver.Current.GetService<CategoryService>();
            }
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var categories = _categoryService.GetAll();

            foreach (var item in categories)
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = item.SeoName;
                dynamicNode.ParentKey = "Home";
                dynamicNode.RouteValues.Add("id", item.Id);
                dynamicNode.RouteValues.Add("category", item.SeoName);

                yield return dynamicNode;
            }
        }
    }

    public class GaleryDetailDynamicNodeProvider : DynamicNodeProviderBase
    {
        public IGaleryService _galeryService
        {
            get
            {
                return DependencyResolver.Current.GetService<GaleryService>();
            }
        }

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var categories = _galeryService.GetAll();

            foreach (var item in categories)
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = item.SeoName;
                dynamicNode.ParentKey = "Home";
                dynamicNode.RouteValues.Add("id", item.Id);
                dynamicNode.RouteValues.Add("seoName", item.SeoName);

                yield return dynamicNode;
            }
        }
    }
}

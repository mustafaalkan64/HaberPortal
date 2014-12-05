using Best.Data.UnitOfWork;
using Best.Service.CategoryServices;
using Best.Web.Infrastructure.Controllers;
using DevTrends.MvcDonutCaching;
using System.Web.Mvc;
using System.Web.UI;

namespace Best.Web.Controllers
{
    public class CategoryController : PublicController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IUnitOfWork uow, ICategoryService categoryService)
            : base(uow)
        {
            _categoryService = categoryService;
        }
        
        [DonutOutputCache(Duration = 3600, VaryByParam = "id", Location = OutputCacheLocation.Client)]
        public ActionResult CategoryDetail(int id)
        {
            var category = _categoryService.Find(id);

            return View(category);
        }
    }
}
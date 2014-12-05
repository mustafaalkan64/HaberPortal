using Best.Data.UnitOfWork;
using Best.Service.CategoryServices;
using Best.Service.GaleryServices;
using Best.Service.NewsServices;
using Best.Web.Infrastructure.Controllers;
using DevTrends.MvcDonutCaching;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Best.Web.Controllers
{
    public class HomeController : PublicController
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly IGaleryService _galeryService;

        public HomeController(IUnitOfWork uow, INewsService newsService, ICategoryService categoryService, IGaleryService galeryService)
            : base(uow)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _galeryService = galeryService;
        }

        [DonutOutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _LeftHeadline()
        {
            var news = _newsService.GetAll(1, 2);

            return PartialView(news);
        }

        public ActionResult _MiddleHeadline()
        {
            var news = _newsService.GetAll(2, 15);

            return PartialView(news);
        }

        public ActionResult _RightHeadline()
        {
            var news = _newsService.GetAll(3, 3);

            return PartialView(news);
        }

        public ActionResult _NewsByCategory()
        {
            var categories = _categoryService.GetAll();

            return PartialView(categories.OrderBy(x => x.Order));
        }

        public ActionResult _MostReaded()
        {
            var news = _newsService.GetMostReaded(10);

            return PartialView(news);
        }

        public ActionResult _CategoriesMenu()
        {
            var categories = _categoryService.GetAll();

            return PartialView(categories);
        }

        public ActionResult _LastPhotoGalery()
        {
            var galeries = _galeryService.GetLastAddedGaleries(2);

            return PartialView(galeries);
        }
    }
}
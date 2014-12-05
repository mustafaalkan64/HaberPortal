using Best.Data.UnitOfWork;
using Best.Service.GaleryServices;
using Best.Web.Infrastructure.Controllers;
using Best.Web.Models;
using DevTrends.MvcDonutCaching;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Best.Web.Controllers
{
    public class GaleryController : PublicController
    {
        private readonly IGaleryService _galeryService;

        public GaleryController(IUnitOfWork uow, IGaleryService galeryService)
            : base(uow)
        {
            _galeryService = galeryService;
        }
        
        [DonutOutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client)]
        public ActionResult Galeries()
        {
            var galeries = _galeryService.GetAll()
                .Where(x => x.IsActive);

            return View(galeries.OrderByDescending(x => x.Id));
        }

        [DonutOutputCache(Duration = 3600, VaryByParam = "id;seoName;galeryName", Location = OutputCacheLocation.Client)]
        public ActionResult GaleryDetail(int id, string seoName, string galeryName)
        {
            TempData["GaleryName"] = galeryName ?? TempData["GaleryName"];

            var galeryImages = _galeryService.GetAllImages(id);
            var model = new GaleryModel
            {
                Id = id,
                Name = seoName,
                SeoName = seoName,
                GaleryImages = galeryImages.OrderBy(x => x.Id)
            };

            return View(model);
        }
    }
}
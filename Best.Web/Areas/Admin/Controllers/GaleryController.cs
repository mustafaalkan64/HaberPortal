using Best.Core.Domain.DbEntities;
using Best.Data.UnitOfWork;
using Best.Service.GaleryServices;
using Best.Utilities.ImageOperations;
using Best.Utilities.StringOperations;
using Best.Web.Areas.Admin.Models.GaleryModels;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Best.Web.Areas.Admin.Controllers
{
    public class GaleryController : AdminController
    {
        private readonly IGaleryService _galeryService;

        public GaleryController(IUnitOfWork uow, IGaleryService galeryService)
            : base(uow)
        {
            _galeryService = galeryService;
        }

        public ActionResult Index()
        {
            var galeries = _galeryService.GetAll();

            return View(galeries.OrderBy(x => x.Id));
        }

        public ActionResult AddGalery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGalery(EditGaleryModel model)
        {
            if (ModelState.IsValid)
            {
                var galery = new Galery();
                galery.Description = model.Description;
                galery.InsertDate = DateTime.Now;
                galery.InsertUserId = 0;
                galery.IsActive = model.IsActive;
                galery.Name = model.Name;
                galery.UpdateDate = DateTime.Now;
                galery.UpdateUserId = 0;
                galery.SeoName = StringManager.ToSeoFriendlyString(model.Name);

                _galeryService.Insert(galery);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult EditGalery(int id)
        {
            var galery = _galeryService.Find(id);
            var model = new EditGaleryModel();
            model.Id = galery.Id;
            model.Description = galery.Description;
            model.IsActive = galery.IsActive;
            model.Name = galery.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditGalery(EditGaleryModel model)
        {
            if (ModelState.IsValid)
            {
                var galery = _galeryService.Find(model.Id);
                galery.Description = model.Description;
                galery.IsActive = model.IsActive;
                galery.Name = model.Name;
                galery.UpdateDate = DateTime.Now;
                galery.UpdateUserId = 0;
                galery.SeoName = StringManager.ToSeoFriendlyString(model.Name);

                _galeryService.Update(galery);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult GaleryImages(int id, string name)
        {
            var images = _galeryService.GetImagesByGalery(id);

            ViewBag.GaleryId = id;
            ViewBag.Name = name;

            return View(images.OrderBy(x => x.Id));
        }

        [HttpPost]
        public ActionResult AddImages(int galeryId, string galeryName, List<HttpPostedFileBase> images)
        {
            if (images != null)
            {
                foreach (var item in images)
                {
                    var image = item;
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                    var seoGaleryName = StringManager.ToSeoFriendlyString(galeryName);
                    var imageDirectory = Server.MapPath("~/Content/Images/uploads/Galery/" + seoGaleryName);
                    var imageDirectoryBig = Server.MapPath("~/Content/Images/uploads/Galery/" + seoGaleryName + "/Big");
                    var imageDirectoryMiddle = Server.MapPath("~/Content/Images/uploads/Galery/" + seoGaleryName + "/Middle");
                    var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/Galery/" + seoGaleryName + "/Small");

                    // dizin yoksa oluştur.
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                        Directory.CreateDirectory(imageDirectoryBig);
                        Directory.CreateDirectory(imageDirectoryMiddle);
                        Directory.CreateDirectory(imageDirectorySmall);
                    }

                    // resmi sunucuya kaydet
                    image.SaveAs(Path.Combine(imageDirectory, fileName));

                    // resmi küçük boyutta kaydet
                    ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(150, 150), imageDirectorySmall, fileName);
                    ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(450, 450), imageDirectoryMiddle, fileName);
                    ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(750, 750), imageDirectoryBig, fileName);

                    var galeryImage = new GaleryImage();
                    galeryImage.ContentLength = item.ContentLength;
                    galeryImage.ContentType = item.ContentType;
                    galeryImage.Description = "";
                    galeryImage.GaleryId = galeryId;
                    galeryImage.ImgUrlOriginal = Path.Combine("Content/Images/uploads/Galery/" + seoGaleryName + "/", fileName);
                    galeryImage.ImgUrlBig = Path.Combine("Content/Images/uploads/Galery/" + seoGaleryName + "/Big/", fileName);
                    galeryImage.ImgUrlMiddle = Path.Combine("Content/Images/uploads/Galery/" + seoGaleryName + "/Middle/", fileName);
                    galeryImage.ImgUrlSmall = Path.Combine("Content/Images/uploads/Galery/" + seoGaleryName + "/Small/", fileName);
                    galeryImage.InsertDate = DateTime.Now;
                    galeryImage.InsertUserId = 0;
                    galeryImage.IsActive = true;
                    galeryImage.Name = fileName;
                    galeryImage.UpdateDate = DateTime.Now;
                    galeryImage.UpdateUserId = 0;

                    _galeryService.InsertGaleryImage(galeryImage);
                    _uow.SaveChanges();
                }
            }

            return RedirectToAction("GaleryImages", new { id = galeryId, name = galeryName });
        }

        public ActionResult EditGaleryImage(int id, int galeryId, string galeryName)
        {
            var galeryImage = _galeryService.FindGaleryImage(id);

            var model = new EditGaleryImageModel
            {
                Id = id,
                GaleryName = galeryName,
                GaleryId = galeryId,
                Description = galeryImage.Description,
                ImgUrl = galeryImage.ImgUrlSmall
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditGaleryImage(EditGaleryImageModel model)
        {
            if (ModelState.IsValid)
            {
                var galeryImage = _galeryService.FindGaleryImage(model.Id);
                galeryImage.Description = model.Description;
                galeryImage.UpdateDate = DateTime.Now;
                galeryImage.UpdateUserId = 0;

                _galeryService.UpdateGaleryImage(galeryImage);
                _uow.SaveChanges();

                return RedirectToAction("GaleryImages", new { id = model.GaleryId, name = model.GaleryName });
            }

            return View(model);
        }
    }
}
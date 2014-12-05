using Best.Core.Domain.DbEntities;
using Best.Data.UnitOfWork;
using Best.Service.CategoryServices;
using Best.Utilities.ImageOperations;
using Best.Web.Areas.Admin.Models.CategoryModels;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Linq;
using Best.Utilities.StringOperations;

namespace Best.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IUnitOfWork uow, ICategoryService categoryService)
            : base(uow)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetAll();

            return View(categories.OrderBy(x => x.Id));
        }

        public ActionResult AddCategory()
        {
            EditCategoryModel model = new EditCategoryModel();
            model.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(EditCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var image = model.ProfileImg;
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                var imageDirectory = Server.MapPath("~/Content/Images/uploads/Category");
                var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/Category/Small");

                // dizin yoksa oluştur.
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                    Directory.CreateDirectory(imageDirectorySmall);
                }

                // resmi sunucuya kaydet
                image.SaveAs(Path.Combine(imageDirectory, fileName));

                // resmi küçük boyutta kaydet
                ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(300, 300), imageDirectorySmall, fileName);

                var category = new Category();
                category.Description = model.Description;
                category.ProfileImgUrl = Path.Combine("Content/Images/uploads/Category/Small/", fileName);
                category.InsertDate = DateTime.Now;
                category.InsertUserId = 0;
                category.IsActive = model.IsActive;
                category.Name = model.Name;
                category.NodePathIds = "";
                category.NodePathText = "";
                category.Order = model.Order;
                category.ParentId = model.ParentId;
                category.SeoName = StringManager.ToSeoFriendlyString(model.Name);
                category.UpdateDate = DateTime.Now;
                category.UpdateUserId = 0;

                _categoryService.Insert(category);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Categories = _categoryService.GetAll();

            return View(model);
        }

        public ActionResult EditCategory(int id)
        {
            var category = _categoryService.Find(id);
            EditCategoryModel model = new EditCategoryModel();

            model.Categories = _categoryService.GetAll();
            model.Description = category.Description;
            model.Id = category.Id;
            model.IsActive = category.IsActive;
            model.Name = category.Name;
            model.Order = category.Order;
            model.ParentId = category.ParentId;
            model.ProfileImgUrl = category.ProfileImgUrl;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(EditCategoryModel model)
        {
            ModelState.Remove("ProfileImg");

            if (ModelState.IsValid)
            {
                var category = _categoryService.Find(model.Id);

                if (model.ProfileImg != null)
                {
                    var image = model.ProfileImg;
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                    var imageDirectory = Server.MapPath("~/Content/Images/uploads/Category");
                    var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/Category/Small");

                    // dizin yoksa oluştur.
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                        Directory.CreateDirectory(imageDirectorySmall);
                    }

                    // resmi sunucuya kaydet
                    image.SaveAs(Path.Combine(imageDirectory, fileName));

                    // resmi küçük boyutta kaydet
                    ImageManager.SaveResizedImage(Image.FromFile(Path.Combine(imageDirectory, fileName)), new Size(300, 300), imageDirectorySmall, fileName);
                    category.ProfileImgUrl = Path.Combine("Content/Images/uploads/Category/Small/", fileName);
                }

                category.Description = model.Description;
                category.IsActive = model.IsActive;
                category.Name = model.Name;
                category.NodePathIds = "";
                category.NodePathText = "";
                category.Order = model.Order;
                category.ParentId = model.ParentId;
                category.SeoName = StringManager.ToSeoFriendlyString(model.Name);
                category.UpdateDate = DateTime.Now;
                category.UpdateUserId = 0;

                _categoryService.Update(category);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Categories = _categoryService.GetAll();

            return View(model);
        }
    }
}
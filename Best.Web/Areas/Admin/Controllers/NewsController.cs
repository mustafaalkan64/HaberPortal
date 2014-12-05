using Best.Core.Domain.DbEntities;
using Best.Data.UnitOfWork;
using Best.Service.CategoryServices;
using Best.Service.GaleryServices;
using Best.Service.NewsServices;
using Best.Service.TagService;
using Best.Service.UserServices;
using Best.Utilities.ImageOperations;
using Best.Utilities.StringOperations;
using Best.Web.Areas.Admin.Models.NewsModels;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Best.Web.Areas.Admin.Controllers
{
    public class NewsController : AdminController
    {
        private readonly ICategoryService _categoryService;
        private readonly IGaleryService _galeryService;
        private readonly INewsService _newsService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public NewsController(IUnitOfWork uow,
            ICategoryService categoryService,
            IGaleryService galeryService,
            INewsService newsService,
            ITagService tagService,
            IUserService userService)
            : base(uow)
        {
            _categoryService = categoryService;
            _galeryService = galeryService;
            _newsService = newsService;
            _tagService = tagService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            var news = _newsService.GetAll();

            return View(news.OrderByDescending(x => x.Id));
        }

        public ActionResult AddNews()
        {
            var model = InitializeNewsModel(new EditNewsModel());
            model.IsPublished = true;

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNews(EditNewsModel model)
        {
            if (ModelState.IsValid)
            {
                var tags = _tagService.GetAll(model.SelectedTagIds);
                var galeries = _galeryService.GetAll(model.SelectedGaleryIds);

                var image = model.ProfileImg;
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);
                var seoTitle = StringManager.ToSeoFriendlyString(model.Title);
                var imageDirectory = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle);
                var imageDirectoryBig = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Big");
                var imageDirectoryMiddle = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Middle");
                var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Small");

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

                var news = new News();
                news.AuthorId = model.AuthorId;
                news.CategoryId = model.CategoryId;
                news.CommentCount = 0;
                news.Content = model.Content;
                news.Description = model.Description;
                news.InsertDate = DateTime.Now;
                news.InsertUserId = 0;
                news.IsActive = true;
                news.IsPublished = model.IsPublished;
                news.NewsPositionId = model.NewsPositionId;
                news.NewsTypeId = model.NewsTypeId;
                news.PublishDate = DateTime.Now;
                news.PublishUserId = 0;
                news.ReadCount = 0;
                news.SeoTitle = seoTitle;
                news.ShortDescription = model.ShortDescription;
                news.Source = model.Source;
                news.TagNames = String.Join(",", tags.Select(x => x.Name));
                news.Title = model.Title;
                news.UpdateDate = DateTime.Now;
                news.UpdateUserId = 0;
                news.UserId = 2;
                news.ProfileImgUrl = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/", fileName);
                news.ProfileImgUrlBig = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Big/", fileName);
                news.ProfileImgUrlMiddle = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Middle/", fileName);
                news.ProfileImgUrlSmall = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Small/", fileName);

                tags.ForEach(x => news.Tags.Add(x));
                galeries.ForEach(x => news.Galeries.Add(x));

                _newsService.Insert(news);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            model = InitializeNewsModel(model);

            return View(model);
        }

        public ActionResult EditNews(int id)
        {
            var news = _newsService.Find(id);
            var model = new EditNewsModel();

            model = InitializeNewsModel(model);

            model.AuthorId = news.AuthorId;
            model.CategoryId = news.CategoryId;
            model.Content = news.Content;
            model.Description = news.Description;
            model.Id = news.Id;
            model.IsActive = news.IsActive;
            model.IsPublished = news.IsPublished;
            model.NewsPositionId = news.NewsPositionId;
            model.NewsTypeId = news.NewsTypeId;
            model.ProfileImgUrl = news.ProfileImgUrlSmall;
            model.SelectedGaleryIds = news.Galeries.Select(x => x.Id).ToArray();
            model.SelectedTagIds = news.Tags.Select(x => x.Id).ToArray();
            model.ShortDescription = news.ShortDescription;
            model.Source = news.Source;
            model.Title = news.Title;

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditNews(EditNewsModel model)
        {
            ModelState.Remove("ProfileImg");

            if (ModelState.IsValid)
            {
                var news = _newsService.Find(model.Id);
                var seoTitle = StringManager.ToSeoFriendlyString(model.Title);
                var tags = _tagService.GetAll(model.SelectedTagIds);
                var galeries = _galeryService.GetAll(model.SelectedGaleryIds);

                if (model.ProfileImg != null)
                {
                    var image = model.ProfileImg;
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(image.FileName);

                    var imageDirectory = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle);
                    var imageDirectoryBig = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Big");
                    var imageDirectoryMiddle = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Middle");
                    var imageDirectorySmall = Server.MapPath("~/Content/Images/uploads/News/" + seoTitle + "/Small");

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

                    news.ProfileImgUrl = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/", fileName);
                    news.ProfileImgUrlBig = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Big/", fileName);
                    news.ProfileImgUrlMiddle = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Middle/", fileName);
                    news.ProfileImgUrlSmall = Path.Combine("Content/Images/uploads/News/" + seoTitle + "/Small/", fileName);
                }

                news.AuthorId = model.AuthorId;
                news.CategoryId = model.CategoryId;
                news.CommentCount = 0;
                news.Content = model.Content;
                news.Description = model.Description;
                news.InsertDate = DateTime.Now;
                news.InsertUserId = 0;
                news.IsActive = true;
                news.IsPublished = model.IsPublished;
                news.NewsPositionId = model.NewsPositionId;
                news.NewsTypeId = model.NewsTypeId;
                news.PublishDate = DateTime.Now;
                news.PublishUserId = 0;
                news.ReadCount = 0;
                news.SeoTitle = seoTitle;
                news.ShortDescription = model.ShortDescription;
                news.Source = model.Source;
                news.TagNames = String.Join(",", tags.Select(x => x.Name));
                news.Title = model.Title;
                news.UpdateDate = DateTime.Now;
                news.UpdateUserId = 0;
                news.UserId = 2;


                tags.ToList().ForEach(x => news.Tags.Add(x));
                galeries.ToList().ForEach(x => news.Galeries.Add(x));

                _newsService.Update(news);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            model = InitializeNewsModel(model);

            return View(model);
        }

        private EditNewsModel InitializeNewsModel(EditNewsModel model)
        {
            model.Categories = _categoryService.GetAll();
            model.NewsPositions = _newsService.GetAllNewsPositons();
            model.Galeries = _galeryService.GetAll();
            model.Tags = _tagService.GetAll();
            model.Authors = _userService.GetAllAuthors();
            model.NewsTypes = _newsService.GetAllNewsTypes();

            return model;
        }
    }
}
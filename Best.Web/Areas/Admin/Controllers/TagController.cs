using Best.Core.Domain.DbEntities;
using Best.Data.UnitOfWork;
using Best.Service.TagService;
using Best.Utilities.StringOperations;
using Best.Web.Areas.Admin.Models.TagModels;
using Best.Web.Infrastructure.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Best.Web.Areas.Admin.Controllers
{
    public class TagController : AdminController
    {
        private readonly ITagService _tagService;

        public TagController(IUnitOfWork uow, ITagService tagService)
            : base(uow)
        {
            _tagService = tagService;
        }

        public ActionResult Index()
        {
            var tags = _tagService.GetAll();

            return View(tags.OrderBy(x => x.Id));
        }

        public ActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTag(EditTagModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag();
                tag.Description = model.Description;
                tag.InsertDate = DateTime.Now;
                tag.InsertUserId = 0;
                tag.IsActive = model.IsActive;
                tag.Name = model.Name;
                tag.SeoName = StringManager.ToSeoFriendlyString(model.Name);
                tag.UpdateDate = DateTime.Now;
                tag.UpdateUserId = 0;

                _tagService.Insert(tag);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult EditTag(int id)
        {
            var tag = _tagService.Find(id);
            var model = new EditTagModel();
            model.Id = tag.Id;
            model.Description = tag.Description;
            model.IsActive = tag.IsActive;
            model.Name = tag.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditTag(EditTagModel model)
        {
            if (ModelState.IsValid)
            {
                var tag = _tagService.Find(model.Id);
                tag.Description = model.Description;
                tag.IsActive = model.IsActive;
                tag.Name = model.Name;
                tag.SeoName = StringManager.ToSeoFriendlyString(model.Name);
                tag.UpdateDate = DateTime.Now;
                tag.UpdateUserId = 0;

                _tagService.Update(tag);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
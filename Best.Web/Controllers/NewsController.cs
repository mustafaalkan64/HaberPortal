using Best.Core.Domain.DbEntities;
using Best.Data.Context;
using Best.Data.UnitOfWork;
using Best.Service.NewsServices;
using Best.Web.Infrastructure.Controllers;
using Best.Web.Models;
using DevTrends.MvcDonutCaching;
using System;
using System.Web.Mvc;
using System.Web.UI;

namespace Best.Web.Controllers
{
    public class NewsController : PublicController
    {
        private readonly INewsService _newsService;

        public NewsController(IUnitOfWork uow, INewsService newsService)
            : base(uow)
        {
            _newsService = newsService;
        }

        [DonutOutputCache(Duration = 3600, VaryByParam = "id", Location = OutputCacheLocation.Client)]
        public ActionResult NewsDetail(int id)
        {
            var news = _newsService.Find(id);
            news.ReadCount++;

            _newsService.Update(news);
            _uow.SaveChanges();

            return View(news);
        }

        [HttpPost]
        public ActionResult AddComment(EditCommentModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment();

                comment._Content = model.CommentBody;
                comment.InsertDate = DateTime.Now;
                comment.InsertUserId = 0;
                comment.IsActive = true;
                comment.NewsId = model.NewsId;
                //comment.News = context.News.Find(model.NewsId);
                comment.UpdateDate = DateTime.Now;
                comment.UpdateUserId = 0;
                comment.UserId = 2;
                _newsService.InsertComment(comment);
                _uow.SaveChanges();

                return Json(new { redirectTo = Url.Action("NewsDetail", new { id = model.NewsId }) });
            }

            return PartialView("_NewComment", model);
        }
    }
}
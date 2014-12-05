using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Service.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly IGenericRepository<News> _newsRepository;
        private readonly IGenericRepository<NewsPosition> _newsPositionRepository;
        private readonly IGenericRepository<NewsType> _newsTypeRepository;
        private readonly IGenericRepository<Comment> _commentRepository;

        public NewsService(IUnitOfWork uow)
        {
            _newsRepository = uow.GetRepository<News>();
            _newsPositionRepository = uow.GetRepository<NewsPosition>();
            _newsTypeRepository = uow.GetRepository<NewsType>();
            _commentRepository = uow.GetRepository<Comment>();
        }

        /// <summary>
        /// Tüm haberler.
        /// </summary>
        /// <returns></returns>
        public IQueryable<News> GetAll()
        {
            return _newsRepository.GetAll();
        }

        /// <summary>
        /// Tüm haber pozisyonlar.
        /// </summary>
        /// <returns></returns>
        public IQueryable<NewsPosition> GetAllNewsPositons()
        {
            return _newsPositionRepository.GetAll();
        }

        /// <summary>
        /// Tüm haber tipleri.
        /// </summary>
        /// <returns></returns>
        public IQueryable<NewsType> GetAllNewsTypes()
        {
            return _newsTypeRepository.GetAll();
        }

        /// <summary>
        /// Haber ekle.
        /// </summary>
        /// <param name="news"></param>
        public void Insert(News news)
        {
            _newsRepository.Insert(news);
        }

        /// <summary>
        /// Haber bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News Find(int id)
        {
            return _newsRepository.Find(id);
        }

        /// <summary>
        /// Haber güncelle.
        /// </summary>
        /// <param name="news"></param>
        public void Update(News news)
        {
            _newsRepository.Update(news);
        }

        /// <summary>
        /// Pozisyonuna ve sayısına göre haberler.
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="newsCount"></param>
        /// <returns></returns>
        public IQueryable<News> GetAll(int positionId, int newsCount)
        {
            return _newsRepository.GetAll()
                .Where(x => x.IsPublished && x.NewsPosition.PositionId == positionId)
                .OrderByDescending(x => x.Id)
                .Take(newsCount);
        }

        /// <summary>
        /// En çok okunan haberler.
        /// </summary>
        /// <param name="newsCount"></param>
        /// <returns></returns>
        public IQueryable<News> GetMostReaded(int newsCount)
        {
            return _newsRepository.GetAll()
                    .Where(x => x.IsPublished)
                    .OrderByDescending(x => x.Id)
                    .Take(20)
                    .OrderByDescending(x => x.ReadCount)
                    .Take(newsCount);
        }

        /// <summary>
        /// Habere göre tüm yorumlar.
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public IQueryable<Comment> GetAllComments(int newsId)
        {
            return _commentRepository.GetAll()
                .Where(x => x.NewsId == newsId);
        }

        /// <summary>
        /// Yorum ekle.
        /// </summary>
        /// <param name="comment"></param>
        public void InsertComment(Comment comment)
        {
            _commentRepository.Insert(comment);
        }
    }
}

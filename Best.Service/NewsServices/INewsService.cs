using Best.Core.Domain.DbEntities;
using System.Linq;

namespace Best.Service.NewsServices
{
    public interface INewsService
    {
        /// <summary>
        /// Tüm haberler.
        /// </summary>
        /// <returns></returns>
        IQueryable<News> GetAll();

        /// <summary>
        /// Tüm haber pozisyonlar.
        /// </summary>
        /// <returns></returns>
        IQueryable<NewsPosition> GetAllNewsPositons();

        /// <summary>
        /// Tüm haber tipleri.
        /// </summary>
        /// <returns></returns>
        IQueryable<NewsType> GetAllNewsTypes();

        /// <summary>
        /// Haber ekle.
        /// </summary>
        /// <param name="news"></param>
        void Insert(News news);

        /// <summary>
        /// Haber bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        News Find(int id);

        /// <summary>
        /// Haber güncelle.
        /// </summary>
        /// <param name="news"></param>
        void Update(News news);

        /// <summary>
        /// Pozisyonuna ve sayısına göre haberler.
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="newsCount"></param>
        /// <returns></returns>
        IQueryable<News> GetAll(int positionId, int newsCount);

        /// <summary>
        /// En çok okunan haberler.
        /// </summary>
        /// <param name="newsCount"></param>
        /// <returns></returns>
        IQueryable<News> GetMostReaded(int newsCount);

        /// <summary>
        /// Habere göre tüm yorumlar.
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        IQueryable<Comment> GetAllComments(int newsId);

        /// <summary>
        /// Yorum ekle.
        /// </summary>
        /// <param name="comment"></param>
        void InsertComment(Comment comment);
    }
}

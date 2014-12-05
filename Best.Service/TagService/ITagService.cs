using Best.Core.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Service.TagService
{
    public interface ITagService
    {
        /// <summary>
        /// Tüm etiketler.
        /// </summary>
        /// <returns></returns>
        IQueryable<Tag> GetAll();

        /// <summary>
        /// Etiket bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Tag Find(int id);

        /// <summary>
        /// Etiket ekle.
        /// </summary>
        /// <param name="tag"></param>
        void Insert(Tag tag);

        /// <summary>
        /// Etiket güncelle.
        /// </summary>
        /// <param name="tag"></param>
        void Update(Tag tag);

        /// <summary>
        /// Etiket id lerine göre etiketler.
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        List<Tag> GetAll(int[] tagIds);
    }
}

using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Service.TagService
{
    public class TagService : ITagService
    {
        private readonly IGenericRepository<Tag> _tagRepository;

        public TagService(IUnitOfWork uow)
        {
            _tagRepository = uow.GetRepository<Tag>();
        }

        /// <summary>
        /// Tüm etiketler.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        /// <summary>
        /// Etiket bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag Find(int id)
        {
            return _tagRepository.Find(id);
        }

        /// <summary>
        /// Etiket ekle.
        /// </summary>
        /// <param name="tag"></param>
        public void Insert(Tag tag)
        {
            _tagRepository.Insert(tag);
        }

        /// <summary>
        /// Etiket güncelle.
        /// </summary>
        /// <param name="tag"></param>
        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }

        /// <summary>
        /// Etiket id lerine göre etiketler.
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public List<Tag> GetAll(int[] tagIds)
        {
            if (tagIds != null)
            {
                return _tagRepository.GetAll()
                    .Where(x => tagIds.Contains(x.Id))
                    .ToList();
            }

            return new List<Tag>();
        }
    }
}

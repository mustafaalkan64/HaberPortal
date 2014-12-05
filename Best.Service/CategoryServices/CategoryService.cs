using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System.Linq;

namespace Best.Service.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork uow)
        {
            _categoryRepository = uow.GetRepository<Category>();
        }

        /// <summary>
        /// Tüm kategoriler.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        /// <summary>
        /// Kategori bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category Find(int id)
        {
            return _categoryRepository.Find(id);
        }

        /// <summary>
        /// Kategori ekle.
        /// </summary>
        /// <param name="category"></param>
        public void Insert(Category category)
        {
            _categoryRepository.Insert(category);
        }

        /// <summary>
        /// Kategori güncelle.
        /// </summary>
        /// <param name="category"></param>
        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}

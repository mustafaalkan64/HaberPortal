using Best.Core.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Best.Service.CategoryServices
{
    public interface ICategoryService
    {
        /// <summary>
        /// Tüm kategoriler.
        /// </summary>
        /// <returns></returns>
        IQueryable<Category> GetAll();

        /// <summary>
        /// Kategori bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Category Find(int id);

        /// <summary>
        /// Kategori ekle.
        /// </summary>
        /// <param name="category"></param>
        void Insert(Category category);

        /// <summary>
        /// Kategori güncelle.
        /// </summary>
        /// <param name="category"></param>
        void Update(Category category);
    }
}

using Best.Core.Domain.DbEntities;
using System.Collections.Generic;
using System.Linq;

namespace Best.Service.GaleryServices
{
    public interface IGaleryService
    {
        /// <summary>
        /// Tüm galeriler.
        /// </summary>
        /// <returns></returns>
        IQueryable<Galery> GetAll();

        /// <summary>
        /// Galeri bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Galery Find(int id);

        /// <summary>
        /// Galeri ekle.
        /// </summary>
        /// <param name="galery"></param>
        void Insert(Galery galery);

        /// <summary>
        /// Galeri güncelle.
        /// </summary>
        /// <param name="galery"></param>
        void Update(Galery galery);

        /// <summary>
        /// Galeriye ait resimler.
        /// </summary>
        /// <param name="galeryId"></param>
        /// <returns></returns>
        IQueryable<GaleryImage> GetImagesByGalery(int galeryId);

        /// <summary>
        /// Galeri resim ekle.
        /// </summary>
        /// <param name="galeryImage"></param>
        void InsertGaleryImage(GaleryImage galeryImage);

        /// <summary>
        /// Galeri id lerine göre tüm galeriler.
        /// </summary>
        /// <param name="galeryIds"></param>
        /// <returns></returns>
        List<Galery> GetAll(int[] galeryIds);

        /// <summary>
        /// Galeriye göre tüm resimler.
        /// </summary>
        /// <param name="galeryId"></param>
        /// <returns></returns>
        IQueryable<GaleryImage> GetAllImages(int galeryId);

        /// <summary>
        /// Galeri resim bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GaleryImage FindGaleryImage(int id);

        /// <summary>
        /// Galeri resim güncelle.
        /// </summary>
        /// <param name="galeryImage"></param>
        void UpdateGaleryImage(GaleryImage galeryImage);

        /// <summary>
        /// Son eklenen galeriler.
        /// </summary>
        /// <param name="galeryCount"></param>
        /// <returns></returns>
        IQueryable<Galery> GetLastAddedGaleries(int galeryCount);
    }
}

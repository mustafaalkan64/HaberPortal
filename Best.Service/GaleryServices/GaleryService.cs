using Best.Core.Domain.DbEntities;
using Best.Data.Repositories;
using Best.Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Best.Service.GaleryServices
{
    public class GaleryService : IGaleryService
    {
        private readonly IGenericRepository<Galery> _galeryRepository;
        private readonly IGenericRepository<GaleryImage> _galeryImageRepository;

        public GaleryService(IUnitOfWork uow)
        {
            _galeryRepository = uow.GetRepository<Galery>();
            _galeryImageRepository = uow.GetRepository<GaleryImage>();
        }

        /// <summary>
        /// Tüm galeriler.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Galery> GetAll()
        {
            return _galeryRepository.GetAll();
        }

        /// <summary>
        /// Galeri bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Galery Find(int id)
        {
            return _galeryRepository.Find(id);
        }

        /// <summary>
        /// Galeri ekle.
        /// </summary>
        /// <param name="galery"></param>
        public void Insert(Galery galery)
        {
            _galeryRepository.Insert(galery);
        }

        /// <summary>
        /// Galeri güncelle.
        /// </summary>
        /// <param name="galery"></param>
        public void Update(Galery galery)
        {
            _galeryRepository.Update(galery);
        }

        /// <summary>
        /// Galeriye ait resimler.
        /// </summary>
        /// <param name="galeryId"></param>
        /// <returns></returns>
        public IQueryable<GaleryImage> GetImagesByGalery(int galeryId)
        {
            return _galeryImageRepository.GetAll().Where(x => x.GaleryId == galeryId);
        }

        /// <summary>
        /// Galeri resim ekle.
        /// </summary>
        /// <param name="galeryImage"></param>
        public void InsertGaleryImage(GaleryImage galeryImage)
        {
            _galeryImageRepository.Insert(galeryImage);
        }

        /// <summary>
        /// Galeri id lerine göre tüm galeriler.
        /// </summary>
        /// <param name="galeryIds"></param>
        /// <returns></returns>
        public List<Galery> GetAll(int[] galeryIds)
        {
            if (galeryIds != null)
            {
                return _galeryRepository.GetAll()
                    .Where(x => galeryIds.Contains(x.Id))
                    .ToList();
            }

            return new List<Galery>();
        }

        /// <summary>
        /// Galeriye göre tüm resimler.
        /// </summary>
        /// <param name="galeryId"></param>
        /// <returns></returns>
        public IQueryable<GaleryImage> GetAllImages(int galeryId)
        {
            return _galeryImageRepository.GetAll().
                Where(x => x.GaleryId == galeryId);
        }

        /// <summary>
        /// Galeri resim bul.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GaleryImage FindGaleryImage(int id)
        {
            return _galeryImageRepository.Find(id);
        }

        /// <summary>
        /// Galeri resim güncelle.
        /// </summary>
        /// <param name="galeryImage"></param>
        public void UpdateGaleryImage(GaleryImage galeryImage)
        {
            _galeryImageRepository.Update(galeryImage);
        }

        /// <summary>
        /// Son eklenen galeriler.
        /// </summary>
        /// <param name="galeryCount"></param>
        /// <returns></returns>
        public IQueryable<Galery> GetLastAddedGaleries(int galeryCount)
        {
            return _galeryRepository.GetAll()
                .OrderByDescending(x => x.Id)
                .Take(galeryCount);
        }
    }
}

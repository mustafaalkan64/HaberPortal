using Best.Data.Repositories;
using System;

namespace Best.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}

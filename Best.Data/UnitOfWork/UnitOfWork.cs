using Best.Data.Context;
using Best.Data.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Best.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BestContext _context;
        private bool disposed = false;

        public UnitOfWork(BestContext context)
        {
            // veritabanında değişiklik olursa, değişikliği kaydet
            Database.SetInitializer<BestContext>(new Best.Data.Context.BestContext.SeedBestContext());

            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            int affectedRow = 0;

            try
            {
                if (_context == null)
                    throw new ArgumentNullException("_context");

                affectedRow = _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbEx);

                throw fail;
            }

            return affectedRow;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

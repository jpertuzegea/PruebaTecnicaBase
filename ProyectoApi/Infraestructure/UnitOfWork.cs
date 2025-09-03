//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure
{
    /// <inheritdoc />
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly ContextDB ContextDB;
        private IDbContextTransaction transaction;

        /// <inheritdoc />
        public UnitOfWork(ContextDB _ContextDB)
        {
            this.ContextDB = _ContextDB;
        }
       
        /// <inheritdoc />
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(ContextDB);
        }
       
        /// <inheritdoc />
        public void BeginTransaction()
        {
            transaction = this.ContextDB.Database.BeginTransaction();
        }
       
        /// <inheritdoc />
        public void CommitTransaction()
        {
            transaction.Commit();
        }
       
        /// <inheritdoc />
        public void RollbackTransaction()
        {
            transaction.RollbackAsync();
        }



        /// <inheritdoc />
        public bool SaveChanges()
        {
            return ContextDB.SaveChanges() > 0;
        }
       
        /// <inheritdoc />
        public async Task<int> SaveChangesAsync()
        {
            return await ContextDB.SaveChangesAsync();
        }
       
        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ContextDB.Dispose();
                }
                disposedValue = true;
            }
        }
      
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

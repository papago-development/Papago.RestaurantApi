using System;
using System.Collections.Generic;
using System.Linq;

namespace Papago.Data.DataAccess
{
    public interface IPapagoDbContext : IDisposable
    {
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;

        IQueryable<TEntity> GetWithProperties<TEntity>() where TEntity : class;

        TEntity Create<TEntity>( TEntity entity ) where TEntity : class;

        void Update<TEntity>( TEntity originalEntity, TEntity updatedEntity, IEnumerable<string> propertiesToUpdate ) where TEntity : class;

        TEntity Delete<TEntity>( TEntity entity ) where TEntity : class;

        int SaveChanges();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Papago.Model.BaseEntities;

namespace Papago.Business.Services
{
    public interface IEntityService<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> List();
        TEntity Get( int id );
        IEnumerable<TEntity> FindBy( Expression<Func<TEntity, bool>> predicate );
        TEntity Create( TEntity entity, string updatedBy );
        bool Update( TEntity entity, int id, IEnumerable<string> propertiesToUpdate, string updatedBy );
        bool Delete( int id );
    }
}
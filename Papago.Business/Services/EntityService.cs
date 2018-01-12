using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Papago.Data.DataAccess;
using Papago.Model.BaseEntities;

namespace Papago.Business.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : BaseEntity
    {
        private readonly IPapagoDbContext _papagoDbContext;

        public EntityService( IPapagoDbContext papagoDbContext )
        {
            _papagoDbContext = papagoDbContext;
        }

        public virtual IQueryable<TEntity> List() => _papagoDbContext.GetWithProperties<TEntity>();

        public TEntity Get( int id ) => _papagoDbContext.GetWithProperties<TEntity>().FirstOrDefault( x => x.Id == id );

        public IEnumerable<TEntity> FindBy( Expression<Func<TEntity, bool>> predicate )
            => _papagoDbContext.GetWithProperties<TEntity>().Where( predicate ).AsEnumerable();

        public virtual TEntity Create( TEntity entity, string updatedBy )
        {
            entity.CreationDate = DateTime.Now;
            entity.LastUpdate = DateTime.Now;
            entity.UpdatedBy = updatedBy;

            var retval = _papagoDbContext.Create( entity );
            if ( retval != null )
            {
                //try
                //{
                _papagoDbContext.SaveChanges();
                //}
                //catch ( DbUpdateException )
                //{
                //    // do something
                //}
            }

            return retval;
        }

        public virtual bool Update( TEntity entity, int id, IEnumerable<string> propertiesToUpdate, string updatedBy )
        {
            entity.LastUpdate = DateTime.Now;
            entity.UpdatedBy = updatedBy;
            var completeListOfProperties = propertiesToUpdate.ToList();
            completeListOfProperties.AddRange( new List<string> {"UpdatedBy", "LastUpdate"} );
            var existingEntity = Get( id );

            if ( existingEntity != null )
            {
                _papagoDbContext.Update( existingEntity, entity, completeListOfProperties );
                _papagoDbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public virtual bool Delete( int id )
        {
            var entity = Get( id );
            if ( entity == null )
            {
                return false;
            }

            var deletedEntity = _papagoDbContext.Delete( entity );
            if ( deletedEntity != null )
            {
                _papagoDbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
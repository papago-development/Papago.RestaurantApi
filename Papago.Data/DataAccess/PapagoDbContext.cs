using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Papago.Model.BaseEntities;
using Papago.Model.Entities;

namespace Papago.Data.DataAccess
{
    public class PapagoDbContext : DbContext, IPapagoDbContext
    {
        public PapagoDbContext( DbContextOptions dbContextOptions ) : base( dbContextOptions )
        { }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class => Set<TEntity>();

        public IQueryable<TEntity> GetWithProperties<TEntity, TParentEntity>( Expression<Func<TEntity, TParentEntity>> includeExpression ) where TEntity : class
            => Set<TEntity>().Include( includeExpression );

        public IQueryable<TEntity> GetWithProperties<TEntity>() where TEntity : class
        {
            var parentEntityNames = typeof( TEntity ).GetProperties().Where( x => x.PropertyType.IsSubclassOf( typeof( BaseEntity ) ) ).Select( x => x.Name );
            //var include = String.Join( '.', parentEntityNames );
            //return String.IsNullOrEmpty( include )
            //    ? Get<TEntity>()
            //    : Set<TEntity>().Include( include );
            var query = Set<TEntity>().AsQueryable();
            foreach ( var parentEntityName in parentEntityNames )
            {
                query = query.Include( parentEntityName );
            }

            return query;
        }

        public TEntity Create<TEntity>( TEntity entity ) where TEntity : class => Set<TEntity>().Add( entity ).Entity;

        public void Update<TEntity>( TEntity originalEntity, TEntity updatedEntity, IEnumerable<string> propertiesToUpdate ) where TEntity : class
        {
            foreach ( var propertyToUpdate in propertiesToUpdate )
            {
                var dbProperty = Entry( originalEntity ).Property( propertyToUpdate );
                var updatedPropertyInfo = updatedEntity.GetType().GetProperty( propertyToUpdate );
                if ( dbProperty == null || updatedPropertyInfo == null )
                {
                    continue;
                }

                var newValue = updatedPropertyInfo.GetValue( updatedEntity, null );
                if ( newValue?.ToString() != dbProperty.OriginalValue?.ToString() )
                {
                    dbProperty.IsModified = true;
                    dbProperty.CurrentValue = newValue;
                }
            }
        }

        public TEntity Delete<TEntity>( TEntity entity ) where TEntity : class => Set<TEntity>().Remove( entity ).Entity;

        int IPapagoDbContext.SaveChanges() => base.SaveChanges();


        public DbSet<Category> Category { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}
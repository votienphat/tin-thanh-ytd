using System.Data.Entity;

namespace DataAccess.Infrastructure.Contract
{
    public interface IBaseRepository<TContext, TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        void Add(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entity);

        DbSet<TEntity> Entities { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry(TEntity entity);

        TEntity GetOne(object id);

        void Save(TEntity entity);
    }
}
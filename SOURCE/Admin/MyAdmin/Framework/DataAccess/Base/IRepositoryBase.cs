/**********************************************************************
 * Author: ThongNT
 * DateCreate: 06-25-2014 
 * Description: Repository Partten  
 * ####################################################################
 * Author:......................
 * DateModify: .................
 * Description: ................
 * 
 *********************************************************************/

using System.Data.Entity;

namespace DataAccess.Base
{
    internal interface IRepositoryBase<TConcext , TEntity>
     where TEntity : class
        where TConcext: DbContext
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

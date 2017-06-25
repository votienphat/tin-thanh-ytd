using System.Data.Entity;
using DataAccess.Repositories.Infrastructure.Contract;
using System;

namespace DataAccess.Repositories.Infrastructure
{
    public abstract class DaoRepository<TContext, TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        private IBaseDao<TContext, TEntity> _baseDao;
        private IBaseDao<TContext, TEntity> BaseDao
        {
            get { return _baseDao ?? (_baseDao = new BaseDao<TContext, TEntity>()); }
        }

        internal IUnitOfWork<TContext, TEntity> Uow
        {
            get
            {
                return BaseDao.Uow;
            }
        }

        internal void SetUow(string connectionStringOrName)
        {
            if (!String.IsNullOrEmpty(connectionStringOrName))
            {
                connectionStringOrName = string.Format("{0}initial catalog={1};", connectionStringOrName, Uow.Context.Database.Connection.Database);
                _baseDao = new BaseDao<TContext, TEntity>(connectionStringOrName);        
            }
        }

        public TEntity GetOne(object id, int areaId = 0)
        {
            return BaseDao.GetOne(id);
        }

        public bool Add(TEntity entity)
        {
            return BaseDao.Add(entity);
        }

        public void DraftAdd(TEntity entity)
        {
            BaseDao.DraftAdd(entity);
        }

        public bool Delete(object id)
        {
            return BaseDao.Delete(id);
        }

        public void DraftDelete(object id)
        {
            BaseDao.DraftDelete(id);
        }

        public bool Delete(TEntity entity)
        {
            return BaseDao.Delete(entity);
        }

        public void DraftDelete(TEntity entity)
        {
            BaseDao.DraftDelete(entity);
        }

        public bool Save(TEntity entity)
        {
            return BaseDao.Save(entity);
        }

        public void DraftSave(TEntity entity)
        {
            BaseDao.DraftSave(entity);
        }
    }
}
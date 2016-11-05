using System.Data.Entity;
using DataAccess.Infrastructure.Contract;

namespace DataAccess.Infrastructure
{
    internal class BaseDao<TContext, TEntity> : IBaseDao<TContext, TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {
        /// <summary>
        /// Get One Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetOne(object id)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                return repository.GetOne(id);
            }
        }

        /// <summary>
        /// Add then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Add(entity);
                return Uow.CommitChanges() > 0;
            }
        }

        /// <summary>
        /// Add without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DraftAdd(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Add(entity);
            }
        }

        /// <summary>
        /// Delete then commit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(object id)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Delete(id);
                return Uow.CommitChanges() > 0;
            }
        }

        /// <summary>
        /// Delete without commit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DraftDelete(object id)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Delete(id);
            }
        }

        /// <summary>
        /// Delete then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Delete(entity);
                return Uow.CommitChanges() > 0;
            }
        }

        /// <summary>
        /// Delete without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DraftDelete(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Delete(entity);
            }
        }

        /// <summary>
        /// Save then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Save(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Save(entity);
                return Uow.CommitChanges() > 0;
            }
        }

        /// <summary>
        /// Save without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DraftSave(TEntity entity)
        {
            //using (Uow)
            {
                var repository = Uow.GetRepository();
                repository.Save(entity);
            }
        }

        public int CommitChanges()
        {
            //using (Uow)
            {
                return Uow.CommitChanges();
            }
        }

        private IUnitOfWork<TContext, TEntity> _unitOfWork;
        public IUnitOfWork<TContext, TEntity> Uow
        {
            get
            {
                return _unitOfWork ?? (_unitOfWork = new UnitOfWork<TContext, TEntity>());
            }
            set { _unitOfWork = value; }
        }
    }
}
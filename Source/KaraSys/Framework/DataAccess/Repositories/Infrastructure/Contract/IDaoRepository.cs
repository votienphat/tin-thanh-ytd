namespace DataAccess.Repositories.Infrastructure.Contract
{
    public interface IDaoRepository<TEntity> where TEntity : class
    {
        TEntity GetOne(object id, int areaId = 0);

        /// <summary>
        /// Add then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(TEntity entity);

        /// <summary>
        /// Add without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void DraftAdd(TEntity entity);

        /// <summary>
        /// Delete then commit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(object id);

        /// <summary>
        /// Delete without commit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DraftDelete(object id);

        /// <summary>
        /// Delete then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Delete without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void DraftDelete(TEntity entity);

        /// <summary>
        /// Save then commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Save(TEntity entity);

        /// <summary>
        /// Save without commit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void DraftSave(TEntity entity);
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using DataAccess.Contract;
using DataAccess.Helper;
using DataAccess.Repositories.Infrastructure.Contract;
using System.Configuration;

namespace DataAccess.Repositories.Infrastructure
{
    internal class UnitOfWork<TContext, TEntity> : IUnitOfWork<TContext, TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public UnitOfWork(string connectionStringOrName = null)
        {
            Context = new TContext();

            /*
            var area = new SessionHelper().ServerAreaID;
            string value = string.Empty;
            switch (area)
            {
                case (int)AreaServerEnum.AreaServerGoogle:
                    value = "data source=10.17.0.28;user id=uweb;password=123456;MultipleActiveResultSets=True;";
                    break;
                case (int)AreaServerEnum.AreaServerVietNam:
                    value = "data source=10.17.0.20;user id=uweb;password=123456;MultipleActiveResultSets=True;"; //ConfigurationManager.AppSettings["ConnectionStringVN"];
                    break;
            }

            if (!string.IsNullOrEmpty(value))
            {
                connectionStringOrName = string.Format("{0}initial catalog={1};", value, Context.Database.Connection.Database);
            }
            */

            if (!string.IsNullOrEmpty(connectionStringOrName))
            {
                Context.Database.Connection.ConnectionString = connectionStringOrName;
            }
        }

        public IBaseRepository<TContext, TEntity> GetRepository()
        {
            if (Context == null)
                Context = new TContext();
            IBaseRepository<TContext, TEntity> repository = new BaseRepository<TContext, TEntity>(Context);
            return repository;
        }

        public TransactionScope BeginTransaction(IsolationLevel isolationLevel)
        {
            var options = new TransactionOptions { IsolationLevel = isolationLevel };
            return new TransactionScope(TransactionScopeOption.RequiresNew, options);
        }

        public TContext Context { get; private set; }

        public int CommitChanges()
        {
            return Context.SaveChanges();
        }

        public IEnumerable<TResult> ExecuteStoredProcedure<TResult>(IStoredProcedure<TResult> procedure, string storeName = null)
        {
            //return Context.Database.ExecuteStoredProcedure(procedure);
            return Enumerable.Empty<TResult>();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }
    }
}
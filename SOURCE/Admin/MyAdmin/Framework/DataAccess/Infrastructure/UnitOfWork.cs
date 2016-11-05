using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using DataAccess.Helper;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities;

namespace DataAccess.Infrastructure
{
    internal class UnitOfWork<TContext, TEntity> : IUnitOfWork<TContext, TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public UnitOfWork(string connectionStringOrName = null)
        {
            Context = new TContext();
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

        //public IEnumerable<TResult> ExecuteStoredProcedure<TResult>(IStoredProcedure<TResult> procedure, string storeName = null)
        //{
        //    return Context.Database.ExecuteStoredProcedure(procedure);
        //}

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
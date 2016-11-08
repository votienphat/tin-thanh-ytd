using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using DataAccess.Contract;

namespace DataAccess.Repositories.Infrastructure.Contract
{
    internal interface IUnitOfWork<TContext, TEntity> : IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        TContext Context { get; }
        IBaseRepository<TContext, TEntity> GetRepository();
        IEnumerable<TResult> ExecuteStoredProcedure<TResult>(IStoredProcedure<TResult> procedure,
            string storeName = null);
        TransactionScope BeginTransaction(IsolationLevel isolationLevel);
        int CommitChanges();
        //void SetIsolationLevel(System.Data.IsolationLevel iso);
    }
}
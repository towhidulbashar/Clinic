using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authorize.Core.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IDbTransaction Transaction { get; set; }
        public IDbConnection Connection { get { return Transaction.Connection; } }
        private string tableName = string.Empty;
        public Repository(IDbTransaction transaction)
        {
            this.Transaction = transaction;
        }
        public abstract Task Add(TEntity entity);
        public abstract void Add(IEnumerable<TEntity> entities);
        public abstract TEntity Get(long id);
        public abstract Task<IEnumerable<TEntity>> Get();
        public abstract void Remove(TEntity entity);
        public abstract void Remove(IEnumerable<TEntity> entities);
        public abstract TEntity SingleOrDefault(Expression<Func<bool, TEntity>> expression);
    }
}

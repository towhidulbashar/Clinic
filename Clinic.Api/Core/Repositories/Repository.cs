using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Api.Core.Repositories
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
        // {
        //     throw new NotImplementedException();
        // }

        public abstract TEntity Get(long id);
        // {
        //     throw new NotImplementedException();
        // }

        public abstract Task<IEnumerable<TEntity>> Get();
        //{
        //    throw new NotImplementedException();
        //}

        public abstract void Remove(TEntity entity);
        // {
        //     throw new NotImplementedException();
        // }

        public abstract void Remove(IEnumerable<TEntity> entities);
        // {
        //     throw new NotImplementedException();
        // }

        public abstract TEntity SingleOrDefault(Expression<Func<bool, TEntity>> expression);
        // {
        //     throw new NotImplementedException();
        // }
    }
}

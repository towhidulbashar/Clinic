using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Api.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        TEntity Get(long id);
        Task<IEnumerable<TEntity>> Get();
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        TEntity SingleOrDefault(Expression<Func<bool, TEntity>> expression);
    }
}

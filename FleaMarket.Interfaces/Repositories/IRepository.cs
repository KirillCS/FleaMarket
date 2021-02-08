using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FleaMarket.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        TEntity GetById(TId id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

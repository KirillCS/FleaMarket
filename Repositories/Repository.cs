using FleaMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FleaMarket
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DatabaseContext context;

        public Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public TEntity GetById(TId id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.Set<TEntity>().Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.context.Set<TEntity>().RemoveRange(entities);
        }
    }
}

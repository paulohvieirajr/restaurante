using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RestaurantContext _dbContext;

        public RepositoryBase(RestaurantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual bool Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public virtual T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual bool Delete(int id)
        {
            _dbContext.Set<T>().Remove(Get(id));
            return _dbContext.SaveChanges() > 0;
        }

        public virtual bool Exist(int id)
        {
            return Get(id) != null;
        }
    }
}

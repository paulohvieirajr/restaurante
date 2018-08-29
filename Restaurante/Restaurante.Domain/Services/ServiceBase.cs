using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T>
    {
        IRepositoryBase<T> _repositorio;

        public ServiceBase(IRepositoryBase<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual T Get(int id)
        {
            return _repositorio.Get(id);
        }

        public virtual bool Insert(T entity)
        {
            return _repositorio.Insert(entity);
        }

        public virtual bool Update(T entity)
        {
            return _repositorio.Update(entity);
        }

        public virtual bool Delete(int id)
        {
            return _repositorio.Delete(id);
        }

        public virtual bool Exist(int id)
        {
            return _repositorio.Exist(id);
        }
    }
}

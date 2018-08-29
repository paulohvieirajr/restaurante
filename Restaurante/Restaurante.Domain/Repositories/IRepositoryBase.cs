using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        bool Insert(T entity);

        bool Update(T entity);

        T Get(int id);

        bool Delete(int id);

        bool Exist(int id);
    }
}

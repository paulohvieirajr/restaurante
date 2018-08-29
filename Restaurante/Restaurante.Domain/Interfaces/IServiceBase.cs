using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Interfaces
{
    public interface IServiceBase<T>
    {
        T Get(int id);

        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(int id);

        bool Exist(int id);
    }
}

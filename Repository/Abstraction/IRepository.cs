using System;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(Object Id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByID(Object Id);
        void DeleteList(IEnumerable<TEntity> models);
    }
}

using Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext db;

        public Repository(DbContext _db)
        {
            this.db = _db;
        }

        // Get All Records of the TEntity (Class)
        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        // Get Record of the TEntity (Class) based provided ID
        public TEntity GetByID(object Id)
        {
            return db.Set<TEntity>().Find(Id);
        }

        // Add New Entity/Record to the database
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        // Delete Entity/Record
        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        // Delete Entity/Record baesd on given ID
        public void DeleteByID(object Id)
        {
            TEntity entity = db.Set<TEntity>().Find(Id);
            Delete(entity);
        }

        // Delete Entities/Records baesd on provided list of entities
        public void DeleteList(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().RemoveRange(entities);
        }

        // Updated the Entity/Record to the databaes
        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }

        // To Run Stored Procedure
        //public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        //{
        //    return db.Database.SqlQuery<TEntity>(query, parameters);
        //}
    }
}

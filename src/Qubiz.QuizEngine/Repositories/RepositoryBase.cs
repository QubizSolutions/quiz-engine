using AutoMapper.Runtime.Extensions;
using Qubiz.QuizEngine.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Qubiz.QuizEngine.Repositories
{
    public interface IRepository
    {
        void Upsert<T>(T entity) where T : class, IEntity;
        void Delete<T>(T entity) where T : class, IEntity;
        void Delete<T>(Guid id) where T : class, IEntity;
        T GetByID<T>(Guid id) where T : class, IEntity;
        IQueryable<T> All<T>() where T : class, IEntity;
        List<T> ExecuteQuery<T>(string query);
        void SaveChanges();
    }
    
    public class RepositoryBase : IRepository, IDisposable
    {
        protected DbContext entityContext;

        public RepositoryBase(DbContext entityContext)
        {
            this.entityContext = entityContext;
        }

        public virtual void Upsert<T>(T entity)
            where T : class, IEntity
        {
            if (entity == null) throw new System.NullReferenceException("Value cannot be null");

            T existingEntity = entityContext.Set<T>().Find(entity.ID);

            if (existingEntity == null)
            {
                entityContext.Set<T>().Add(entity);
            }
            else
            {
				Mapper.Map(entity, existingEntity);
            }

        }

        public virtual void Delete<T>(T entity)
            where T : class, IEntity
        {
            if (entity == null) throw new System.NullReferenceException("Value cannot be null");

            entityContext.Set<T>().Remove(entity);
        }

        public virtual void Delete<T>(Guid id)
            where T : class, IEntity
        {
            T entity = entityContext.Set<T>().Find(id);
            Delete(entity);
        }

        public virtual T GetByID<T>(Guid id)
            where T : class, IEntity
        {
            return entityContext.Set<T>().Find(id);
        }

        public virtual IQueryable<T> All<T>()
            where T : class, IEntity
        {
            return this.entityContext.Set<T>();
        }

        public virtual List<T> ExecuteQuery<T>(string query)
        {
            return entityContext.Database.SqlQuery<T>(query).ToList();
        }
        
        public virtual void SaveChanges()
        {
            entityContext.SaveChanges();
        }

        public void Dispose()
        {

        }
    }
}
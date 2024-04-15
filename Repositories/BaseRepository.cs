using Car_Sharing.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Repositories
{
    internal class BaseRepository<T> where T : class
    {
        public EntityFramework _ef;

        public BaseRepository()
        {
            _ef = new EntityFramework();
        }
        
        public virtual IEnumerable<T> GetAll()
        {
            return _ef.Set<T>();
        }
        public virtual bool SaveChanges()
        {
            return _ef.SaveChanges() > 0;
        }
        public virtual T? GetById(int id)
        {
            return _ef.Set<T>().Find(id);
        }
        public virtual void AddEntity(T entity)
        {
            if (entity != null)
                _ef.Add(entity);
        }
        //public virtual T GetByName(string name )
        //{
        //    return GetAll().Where(entity=>entity.N)
        //        }

        public virtual void RemoveEntity(T entity)
        {
            if (entity != null)
                _ef.Remove(entity);
        }
        //public virtual void UpdateEntity<T>(T entity)
        //{
        //    if (entity != null)
        //        _ef.Update(entity);
        //}   
    }
}

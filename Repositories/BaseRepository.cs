using Car_Sharing.Data;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual void RemoveEntity(T entity)
        {
            if (entity != null)
                _ef.Remove(entity);
        }
    }
}

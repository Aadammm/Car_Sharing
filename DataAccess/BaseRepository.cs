using Microsoft.EntityFrameworkCore;
namespace Car_Sharing.DataAccess
{
    public class BaseRepository<T> where T : class
    {
        protected EntityFramework _ef;

        public BaseRepository()
        {
            _ef = new EntityFramework();
        }

        public virtual void ReloadEntity(T entity)
        {
            _ef.Entry(entity).Reload();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _ef.Set<T>();
        }
        public virtual bool SaveChanges()
        {
            return _ef.SaveChanges() > 0;
        }
        public virtual void Update(T entity)
        {
            _ef.Update(entity);
        }
        public virtual void Remove(T entity)
        {
            _ef.Remove(entity);
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

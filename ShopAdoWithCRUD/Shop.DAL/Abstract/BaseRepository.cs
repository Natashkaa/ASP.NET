using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL.Abstract
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected DbContext db;

        public BaseRepository(DbContext db)
        {
            this.db = db;
        }

        public void CreateOrUpdate(TEntity entity)
        {
            db.Set<TEntity>().AddOrUpdate(entity);
            Save();
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

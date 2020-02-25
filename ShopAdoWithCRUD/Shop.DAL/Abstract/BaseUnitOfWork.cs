using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL
{
    public abstract class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        protected DbContext db;

        public BaseUnitOfWork(DbContext db)
        {
            this.db = db;
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

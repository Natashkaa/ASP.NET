using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopAdoWeb.Models
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        DbContext context;
        public GenericRepository(DbContext con)
        {
            context = con;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
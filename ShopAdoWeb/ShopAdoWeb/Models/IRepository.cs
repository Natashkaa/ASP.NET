using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAdoWeb.Models
{
    public interface IRepository<T> where T : class,new()
    {
        IEnumerable<T> GetAll();
        void Save();
    }
}
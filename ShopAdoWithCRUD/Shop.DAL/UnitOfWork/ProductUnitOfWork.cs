using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL.UnitOfWork
{
    public class ProductUnitOfWork : BaseUnitOfWork
    {
        public IRepository<Good> GoodRepository { get; }
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Manufacturer> ManufacturerRepository { get; }

        public ProductUnitOfWork(DbContext db,
                                 IRepository<Good> goodRepo,
                                 IRepository<Category> categoryRepo,
                                 IRepository<Manufacturer> manufacturerRepo) : base(db)
        {
            this.GoodRepository = goodRepo;
            this.CategoryRepository = categoryRepo;
            this.ManufacturerRepository = manufacturerRepo;
        }
    }
}

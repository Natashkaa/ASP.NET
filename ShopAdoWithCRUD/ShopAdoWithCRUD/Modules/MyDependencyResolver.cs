using Shop_DAL;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_DAL.Repository;

namespace ShopAdoWithCRUD.Modules
{
    public class MyDependencyResolver : IDependencyResolver
    {
        IKernel kernel;

        public MyDependencyResolver()
        {
            kernel = new StandardKernel();
            kernel.Bind<IRepository<Good>>().To<GoodRepository>();
            kernel.Bind<IRepository<Category>>().To<CategoryRepository>();
            kernel.Bind<IRepository<Manufacturer>>().To<ManufacturerRepository>();
            kernel.Bind<IRepository<Photo>>().To<PhotoRepository>();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
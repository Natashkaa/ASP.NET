using ShopAdoDAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdoDAL
{
    class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            //this.SetDatabaseInitializer(new DropCreateDatabaseAlways<ShopAdoContext>());
            //this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}

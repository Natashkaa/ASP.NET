using ShopAdoDAL.Abstract;
using ShopAdoDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdoDAL.Repository
{
    public class ManufacturerRepository : BaseRepository<Manufacturer>
    {
        public ManufacturerRepository() : base( new ShopAdoContext() ) { }
    }
}

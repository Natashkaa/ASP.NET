using ShopAdoDAL.Abstract;
using ShopAdoDAL.Context;
using ShopAdoDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAdoDAL.Repository
{
    public class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository() : base( new ShopAdoContext() ) { }
    }
}

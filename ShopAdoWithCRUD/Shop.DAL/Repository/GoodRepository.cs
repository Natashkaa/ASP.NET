using Shop_DAL.Abstract;
using Shop_DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL.Repository
{
    public class GoodRepository : BaseRepository<Good>
    {
        public GoodRepository() : base(new ShopAdoEntities()) { }
    }
}

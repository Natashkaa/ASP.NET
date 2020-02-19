using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAdoWeb.Models
{
    public class GoodRepository : GenericRepository<Good>
    {
        public GoodRepository() : base(new ShopAdoContext()) { }
    }
}
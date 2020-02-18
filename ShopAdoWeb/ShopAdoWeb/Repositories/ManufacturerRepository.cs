using ShopAdoWeb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAdoWeb.Models
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>
    {
        public ManufacturerRepository() : base(new ShopAdo_Context()) { }
    }
}
using ShopAdoWeb.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAdoWeb.Models
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository() : base(new ShopAdo_Context()) { }
    }
}
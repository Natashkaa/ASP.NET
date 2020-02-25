using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_DAL
{
    public interface IUnitOfWork
    {
        void Save();
    }
}

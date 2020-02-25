using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_BLL
{
    public interface IService<TEntityDto>
    {
        IEnumerable<TEntityDto> GetAll();
        TEntityDto Get(int id);
        void CreateOrUpdate(TEntityDto entity);
        void Delete(TEntityDto entity);
        void Save();
    }
}

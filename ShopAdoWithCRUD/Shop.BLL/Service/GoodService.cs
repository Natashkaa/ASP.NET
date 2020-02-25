using AutoMapper;
using Shop_DAL;
using Shop_DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_BLL
{
    public class GoodService : IService<GoodDto>
    {
        private ProductUnitOfWork uow;
        IMapper mapper;

        public GoodService(ProductUnitOfWork uow)
        {
            this.uow = uow;
            var config = new MapperConfiguration(cfg =>
                        cfg
                           .CreateMap<Good, GoodDto>()
                            .ForMember(x => x.CategoryName,
                                       option => option.MapFrom(x => x.Category.CategoryName ?? null)));
        }
        public void CreateOrUpdate(GoodDto entity)
        {
            var newEntity = mapper.Map<Good>(entity);
            uow.GoodRepository.CreateOrUpdate(newEntity);
        }

        public void Delete(GoodDto entity)
        {
            Good newEntity = mapper.Map<Good>(entity);
            uow.GoodRepository.Delete(newEntity);
        }

        public GoodDto Get(int id)
        {
            var entity = uow.GoodRepository.Get(id);
            return mapper.Map<GoodDto>(entity);
        }

        public IEnumerable<GoodDto> GetAll()
        {
            var res = uow.GoodRepository
                          .GetAll()
                           .ToList()
                            .Select(ent => mapper.Map<GoodDto>(ent));
            return res;
        }

        public void Save()
        {
            uow.Save();
        }
    }
}

using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Implementations
{
    public class ServiceTipoPromocion:IServiceTipoPromocion
    {

        private readonly IRepositoryTipoPromocion _repository;
        private readonly IMapper _mapper;

        public ServiceTipoPromocion(IRepositoryTipoPromocion repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<TipoPromocionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<TipoPromocionDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<TipoPromocionDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<TipoPromocionDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

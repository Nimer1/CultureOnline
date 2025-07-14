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
    public class ServiceProductoPersonalizado : IServiceProductoPersonalizado
    {
        private readonly IRepositoryProductoPersonalizado _repository;
        private readonly IMapper _mapper;

        public ServiceProductoPersonalizado(IRepositoryProductoPersonalizado repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public Task<int> AddAsync(ProductoPersonalizadoDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductoPersonalizadoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ProductoPersonalizadoDTO>(@object);
            return objectMapped;
        }

        public Task<ProductoPersonalizadoDTO> FindByIdChangeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextNumberOrden()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ProductoPersonalizadoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ProductoPersonalizadoDTO>>(list);
            // Return lista 
            return collection;
        }


    }
}


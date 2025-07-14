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
    public class ServiceGeneroProducto : IServiceGeneroProducto
    {

        private readonly IRepositoryGeneroProducto _repository;
        private readonly IMapper _mapper;

        public ServiceGeneroProducto(IRepositoryGeneroProducto repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<GeneroProductoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<GeneroProductoDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<GeneroProductoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<GeneroProductoDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

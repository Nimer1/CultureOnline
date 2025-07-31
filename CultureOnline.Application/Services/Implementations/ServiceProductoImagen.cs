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
    public class ServiceProductoImagen : IServiceProductoImagenes
    {

        private readonly IRepositoryProductoImagen _repository;
        private readonly IMapper _mapper;

        public ServiceProductoImagen(IRepositoryProductoImagen repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<ProductoImagenesDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ProductoImagenesDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ProductoImagenesDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<ProductoImagenesDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

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
    internal class ServiceProductoEstiqueta : IServiceProductoEtiqueta
    {

        private readonly IRepositoryProductoEtiqueta _repository;
        private readonly IMapper _mapper;

        public ServiceProductoEstiqueta(IRepositoryProductoEtiqueta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<ProductoEtiquetasDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ProductoEtiquetasDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ProductoEtiquetasDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ProductoEtiquetasDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

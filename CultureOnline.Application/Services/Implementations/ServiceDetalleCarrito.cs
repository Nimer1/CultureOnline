using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
using AutoMapper;
using CultureOnline.Infraestructure.Repository.Implementations;
using CultureOnline.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CultureOnline.Application.Services.Implementations
{
    public class ServiceDetalleCarrito : IServiceDetalleCarrito
        {
        private readonly IRepositoryDetalleCarrito _repository;
        private readonly IMapper _mapper;
        public ServiceDetalleCarrito(IRepositoryDetalleCarrito repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> AddAsync(DetalleCarritoDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DetalleCarritoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<DetalleCarritoDTO>(@object);
            return objectMapped;
        }

        public Task<DetalleCarritoDTO> FindByIdChangeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextNumberOrden()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DetalleCarritoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<DetalleCarritoDTO>>(list);
            // Return lista 
            return collection;
        }
    }
    

  
    
}

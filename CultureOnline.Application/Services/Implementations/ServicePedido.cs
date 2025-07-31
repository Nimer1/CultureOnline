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
    public class ServicePedido : IservicePedido
    {

        private readonly IRepositoryPedido _repository;
        private readonly IMapper _mapper;

        public ServicePedido(IRepositoryPedido repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public Task<int> AddAsync(PedidoDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PedidoDTO>(@object);
            return objectMapped;
        }

        public Task<PedidoDTO> FindByIdChangeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNextNumberOrden()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PedidoDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<PedidoDTO>>(list);
            // Return lista 
            return collection;
        }


    }
}

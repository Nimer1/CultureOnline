using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Implementations
{
    public class ServicePromocion : IServicePromocion
    {

        private readonly IRepositoryPromocion _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicePromocion> _logger;
        public ServicePromocion(IRepositoryPromocion repository, IMapper mapper,ILogger<ServicePromocion> logger )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = _logger;
        }
        public async Task<PromocionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PromocionDTO>(@object);
            return objectMapped;
        }

        public Task<ICollection<PromocionDTO>> FindByNameAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PromocionDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<PromocionDTO>>(list);
            // Return lista 
            return collection;
        }


    }
}

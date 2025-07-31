using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
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
        public ServicePromocion(IRepositoryPromocion repository, IMapper mapper, ILogger<ServicePromocion> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PromocionDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<PromocionDTO>(@object);
            return objectMapped;
        }

     

        public async Task<ICollection<PromocionDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<PromocionDTO>>(list);
            return collection;
        }

        public async Task AddAsync(PromocionDTO dto)
        {
            var entity = _mapper.Map<Promociones>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, PromocionDTO dto)
        {
            var entity = _mapper.Map<Promociones>(dto);
            await _repository.UpdateAsync(id, entity);
        }
    }
}

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
    public class ServiceReseña : IServiceReseña
    {

        private readonly IRepositoryReseña _repository;
        private readonly IMapper _mapper;

        public ServiceReseña(IRepositoryReseña repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<ReseñaDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ReseñaDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<ReseñaDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ReseñaDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

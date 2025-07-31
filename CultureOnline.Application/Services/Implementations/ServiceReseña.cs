using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
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
            var collection = _mapper.Map<ICollection<ReseñaDTO>>(list);
            // Return lista 
            return collection;
        }


        public async Task AddAsync(ReseñaDTO reseña)
        {
            var entity = _mapper.Map<Reseñas>(reseña);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, ReseñaDTO reseña)
        {
            var entity = _mapper.Map<Reseñas>(reseña);
            entity.Id = id;
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

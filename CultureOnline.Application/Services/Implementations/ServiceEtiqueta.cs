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
    public class ServiceEtiqueta :IServiceEtiqueta
    {

        private readonly IRepositoryEtiqueta _repository;
        private readonly IMapper _mapper;

        public ServiceEtiqueta(IRepositoryEtiqueta repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<AutorDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<AutorDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<AutorDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<AutorDTO>>(list);
            // Return lista 
            return collection;
        }

        Task<EtiquetaDTO> IServiceEtiqueta.FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<EtiquetaDTO>> IServiceEtiqueta.ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}

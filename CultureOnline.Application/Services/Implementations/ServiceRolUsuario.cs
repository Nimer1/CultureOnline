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
    internal class ServiceRolUsuario: IServiceRolUsuario
    {

        private readonly IRepositoryRolUsuario _repository;
        private readonly IMapper _mapper;

        public ServiceRolUsuario(IRepositoryRolUsuario repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<RolUsuarioDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<RolUsuarioDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<RolUsuarioDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<RolUsuarioDTO>>(list);
            // Return lista 
            return collection;
        }
    }
}

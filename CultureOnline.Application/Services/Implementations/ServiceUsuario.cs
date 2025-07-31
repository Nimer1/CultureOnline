using AutoMapper;
using CultureOnline.Application.Config;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Application.Utils;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Implementations
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IRepositoryUsuario _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _options;

        public ServiceUsuario(IRepositoryUsuario repository, IMapper mapper, IOptions<AppConfig> options)
        {
            _repository = repository;
            _mapper = mapper;
            _options = options;
        }

        public async Task<string> AddAsync(UsuarioDTO dto)
        {
            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(dto.Contrasena!, secret);
            // Establecer password DTO
            dto.Contrasena = passwordEncrypted;
            var objectMapped = _mapper.Map<Usuario>(dto);

            return await _repository.AddAsync(objectMapped);
            //throw new NotImplementedException();
        }

       
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<ICollection<UsuarioDTO>> FindByDescriptionAsync(string description)
        {
            var list = await _repository.FindByDescriptionAsync(description);
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            return collection;
        }

        public async Task<UsuarioDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<UsuarioDTO>(@object);
            return objectMapped;
        }


        public async Task<ICollection<UsuarioDTO>> ListAsync()
        {
            var list = await _repository.ListAsync();
            var collection = _mapper.Map<ICollection<UsuarioDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task<UsuarioDTO> LoginAsync(string id, string password)
        {
            UsuarioDTO usuarioDTO = null!;

            // Llave secreta
            string secret = _options.Value.Crypto.Secret;
            // Password encriptado
            string passwordEncrypted = Cryptography.Encrypt(password, secret);

            var @object = await _repository.LoginAsync(id, passwordEncrypted);

            //var @object = await _repository.LoginAsync(id, password);


            if (@object != null)
            {
                usuarioDTO = _mapper.Map<UsuarioDTO>(@object);
            }

            return usuarioDTO;
            //throw new NotImplementedException();
        }

       
        public async Task UpdateAsync(int id, UsuarioDTO dto)
        {
            var @object = await _repository.FindByIdAsync(id);
            if (@object == null)
                throw new Exception("Usuario no encontrado");
            @object.Estado = dto.Estado;
            await _repository.UpdateAsync(@object);
        } 
        public async Task ActualizarUltimoInicioSesionAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);
            if (usuario != null)
            {
                usuario.UltimoInicioSesion = DateTime.Now;
                await _repository.UpdateAsync(usuario);
            }
        }
    }
}

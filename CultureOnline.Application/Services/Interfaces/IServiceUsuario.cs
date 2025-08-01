﻿using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServiceUsuario
    {
        Task<ICollection<UsuarioDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<UsuarioDTO>> ListAsync();
        Task<UsuarioDTO> FindByIdAsync(int id);
        Task<UsuarioDTO> LoginAsync(string id, string password);
        Task<string> AddAsync(UsuarioDTO dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, UsuarioDTO dto);
        Task ActualizarUltimoInicioSesionAsync(int id);
    }
}

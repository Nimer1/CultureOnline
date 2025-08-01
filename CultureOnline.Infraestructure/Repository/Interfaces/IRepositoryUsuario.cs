﻿using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<ICollection<Usuario>> FindByDescriptionAsync(string description);
        Task<ICollection<Usuario>> ListAsync();
        Task<Usuario> FindByIdAsync(int id);

        Task<Usuario> LoginAsync(string id, string password);
        Task<string> AddAsync(Usuario entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(Usuario usuario);
    }
}

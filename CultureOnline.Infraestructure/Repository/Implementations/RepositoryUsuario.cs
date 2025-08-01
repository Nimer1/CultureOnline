﻿using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Implementations
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly CultureOnlineContext _context;

        public RepositoryUsuario(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Usuario entity)
        {
            await _context.Set<Usuario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Correo;
        }

        public async Task DeleteAsync(int id)
        {

            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        public async Task<ICollection<Usuario>> FindByDescriptionAsync(string description)
        {
            var collection = await _context
                                         .Set<Usuario>()
                                         .Where(p => p.Nombre.Contains(description))
                                         .ToListAsync();
            return collection;
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            var usuario = await _context.Set<Usuario>()
                .Include(u => u.IdrolNavigation)
                .FirstOrDefaultAsync(u => u.Id == id);

            return usuario!;
        }



        public async Task<ICollection<Usuario>> ListAsync()
        {
            var collection = await _context.Set<Usuario>()
                                          .Include(p => p.IdrolNavigation)
                                          .ToListAsync();
            return collection;
        }

        public async Task<Usuario> LoginAsync(string id, string password)
        {
            var @object = await _context.Set<Usuario>()
                                        .Include(b => b.IdrolNavigation)
                                        .Where(p => p.Correo == id && p.Contrasena == password)
                                        .FirstOrDefaultAsync();
            if (@object != null && @object.Contrasena.Equals(password, StringComparison.Ordinal))
            {
                return @object;
            }

            return null!;
            //return @object!;
        }

        /*public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }*/
        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}

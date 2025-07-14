using CultureOnline.Infraestructure.Data;
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
    public class RepositoryRolUsuario : IRepositoryRolUsuario
    {
        private readonly CultureOnlineContext _context;

        public RepositoryRolUsuario(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<RolUsuario>> ListAsync()
        {
            return await _context.Set<RolUsuario>().ToListAsync();
        }

        public async Task<RolUsuario> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<RolUsuario>().FindAsync(id);

            return @object!;

        }

    }
}

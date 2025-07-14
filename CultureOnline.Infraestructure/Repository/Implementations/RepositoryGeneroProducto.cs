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
    public class RepositoryGeneroProducto : IRepositoryGeneroProducto
    {
        private readonly CultureOnlineContext _context;

        public RepositoryGeneroProducto(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GeneroProducto>> ListAsync()
        {
            return await _context.Set<GeneroProducto>().ToListAsync();
        }

        public async Task<GeneroProducto> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<GeneroProducto>().FindAsync(id);

            return @object!;

        }

    }
}

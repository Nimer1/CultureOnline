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
    public class RepositoryProductoPersonalizado : IRepositoryProductoPersonalizado
    {
        private readonly CultureOnlineContext _context;
  
        public RepositoryProductoPersonalizado(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ProductoPersonalizado> FindByIdAsync(int id)
        {
            var @object = await _context.Set<ProductoPersonalizado>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<ProductoPersonalizado>> ListAsync()
        {
            return await _context.Set<ProductoPersonalizado>().ToListAsync();
        }
    }
}

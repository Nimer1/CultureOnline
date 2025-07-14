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
    public class RepositoryProductoImagenes : IRepositoryProductoImagen
    {
        private readonly CultureOnlineContext _context;
        public RepositoryProductoImagenes(CultureOnlineContext context)
        {
            _context = context;
        }
        public async Task<ICollection<ProductoImagenes>> ListAsync()
        {
            return await _context.Set<ProductoImagenes>().ToListAsync();
        }
        public async Task<ProductoImagenes> FindByIdAsync(int id)
        {
            var @object = await _context.Set<ProductoImagenes>().FindAsync(id);
            return @object!;
        }

     
    }
    
}

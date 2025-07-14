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
    public class RepositoryProductoEtiqueta : IRepositoryProductoEtiqueta
    {
        private readonly CultureOnlineContext _context;

        public RepositoryProductoEtiqueta(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ProductoEtiquetas>> ListAsync()
        {
            return await _context.Set<ProductoEtiquetas>().ToListAsync();
        }

        public async Task<ProductoEtiquetas> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<ProductoEtiquetas>().FindAsync(id);

            return @object!;

        }

    }
}

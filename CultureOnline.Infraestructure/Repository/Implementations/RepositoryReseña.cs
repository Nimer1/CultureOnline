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
    public class RepositoryReseña : IRepositoryReseña
    {
        private readonly CultureOnlineContext _context;

        public RepositoryReseña(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Reseñas>> ListAsync()
        {
            return await _context.Reseñas
           .Include(r => r.Usuario)
           .Include(r => r.Producto)
           .OrderBy(r => r.Fecha)
           .ToListAsync();
        }

        public async Task<Reseñas> FindByIdAsync(int id)
        {
            var resena = await _context.Reseñas
           .Include(r => r.Usuario)
           .Include(r => r.Producto)
           .FirstOrDefaultAsync(r => r.Id == id);

            return resena!;
        }

    }
}

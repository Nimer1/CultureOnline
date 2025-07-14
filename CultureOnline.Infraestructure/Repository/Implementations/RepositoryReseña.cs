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

<<<<<<< HEAD
        public async Task AddAsync(Reseñas reseña)
        {
            try
            {
                await _context.Reseñas.AddAsync(reseña);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la reseña: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task UpdateAsync(Reseñas reseña)
        {
            _context.Reseñas.Update(reseña);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña != null)
            {
                _context.Reseñas.Remove(reseña);
                await _context.SaveChangesAsync();
            }
        }

=======
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
    }
}

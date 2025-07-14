using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CultureOnline.Infraestructure.Repository.Implementations
{
    public class RepositoryProductoCategorias : IRepositoryProductoCategorias
    {
        private readonly CultureOnlineContext _context;

        public RepositoryProductoCategorias(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoCategorias>> GetAllAsync()
        {
            return await _context.ProductoCategorias
                .Include(pc => pc.Producto)
                .Include(pc => pc.Categoria)
                .ToListAsync();
        }

        public async Task<ProductoCategorias?> GetByIdsAsync(int productoId, int categoriaId)
        {
            return await _context.ProductoCategorias
                .FirstOrDefaultAsync(pc => pc.ProductoId == productoId && pc.CategoriaId == categoriaId);
        }

        public async Task AddAsync(ProductoCategorias entity)
        {
            _context.ProductoCategorias.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productoId, int categoriaId)
        {
            var existing = await GetByIdsAsync(productoId, categoriaId);
            if (existing != null)
            {
                _context.ProductoCategorias.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ProductoCategorias>> ListAsync()
        {
            return await _context.ProductoCategorias
            .Include(pc => pc.Producto)
            .Include(pc => pc.Categoria)
            .ToListAsync();
        }
    }
}

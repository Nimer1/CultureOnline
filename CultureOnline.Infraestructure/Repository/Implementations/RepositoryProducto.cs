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
    public class RepositoryProducto : IRepositoryProducto
    {
        private readonly CultureOnlineContext _context;

        public RepositoryProducto(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<Productos> FindByIdAsync(int id)
        {
            // Incluís relaciones si querés más datos (categorías, etiquetas, etc.)
            var producto = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.Etiqueta)
                .FirstOrDefaultAsync(p => p.Id == id);

            return producto!;
        }

        public async Task<ICollection<Productos>> ListAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.Etiqueta)
                .AsNoTracking()
                .ToListAsync();

            return productos;
        }

        public async Task<int> AddAsync(Productos producto, string[] selectedCategorias)
        {
            if (selectedCategorias != null && selectedCategorias.Length > 0)
            {
                var categorias = await _context.Categorias
                    .Where(c => selectedCategorias.Contains(c.Id.ToString()))
                    .ToListAsync();

                producto.Categorias = categorias;
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto.Id;
        }

    public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) throw new Exception("Producto no encontrado");

            producto.Categorias.Clear(); // Limpia relación que hay de muchos a muchos
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Productos>> GetLibroByCategoria(int idCategoria)
        {
            return await _context.Productos
                .Where(p => p.Categorias.Any(c => c.Id == idCategoria))
                .Include(p => p.Categorias)
                .Include(p => p.Etiqueta)
                .ToListAsync();
        }
        public async Task UpdateAsync(Productos producto, string[] selectedCategorias)
        {
            // Primero busca el producto existente con categorías y autor
            var existingProducto = await _context.Productos
                .Include(p => p.Categorias)
                .Include(p => p.IdAutorNavigation)
                .FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (existingProducto == null)
                throw new Exception("El producto no se ha encontrado");

            // Actualiza las propiedades simples
            _context.Entry(existingProducto).CurrentValues.SetValues(producto);

            // Si el idAutor existe entonces lo actualiza
            if (producto.IdAutor != null)
            {
                var autor = await _context.Autor.FindAsync(producto.IdAutor);
                if (autor != null)
                {
                    existingProducto.IdAutorNavigation = autor;
                    existingProducto.IdAutor = producto.IdAutor;
                }
                else
                {
                    throw new Exception("Autor no encontrado");
                }
            }

            // Actualiza las categorías
            existingProducto.Categorias.Clear();
            if (selectedCategorias != null && selectedCategorias.Length > 0)
            {
                var categorias = await _context.Categorias
                    .Where(c => selectedCategorias.Contains(c.Id.ToString()))
                    .ToListAsync();

                foreach (var cat in categorias)
                {
                    existingProducto.Categorias.Add(cat);
                }
            }

            await _context.SaveChangesAsync();
        }


    }

}

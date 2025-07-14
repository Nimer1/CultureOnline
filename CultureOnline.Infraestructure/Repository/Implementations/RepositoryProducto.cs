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
            var producto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                    .ThenInclude(pc => pc.Categoria)
                .Include(p => p.Etiquetas)
                .Include(p => p.Promociones)
                .Include(p => p.ProductoImagenes)
                .Include(p => p.Reseñas)
                .FirstOrDefaultAsync(p => p.Id == id);

            return producto!;
        }
        public async Task<ICollection<Productos>> ListAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.ProductoCategorias).ThenInclude(pc => pc.Categoria)
                .Include(p => p.Etiquetas)
                .Include(p => p.Promociones)
                .Include(p => p.ProductoImagenes)
                .Include(p => p.IdAutorNavigation)
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

                if (producto.ProductoCategorias == null)
                    producto.ProductoCategorias = new List<ProductoCategorias>();

                foreach (var cat in categorias)
                {
                    producto.ProductoCategorias.Add(new ProductoCategorias
                    {
                        ProductoId = producto.Id, 
                        CategoriaId = cat.Id
                    });
                }
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

            producto.Categorias.Clear(); // Limpia la relación que hay de muchos a muchos
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Productos>> GetLibroByCategoria(int idCategoria)
        {
            return await _context.Productos
                .Where(p => p.Categorias.Any(c => c.Id == idCategoria))
                .Include(p => p.Categorias)
                .Include(p => p.Etiquetas)
                .ToListAsync();
        }
        public async Task UpdateAsync(Productos producto, string[] selectedCategorias)
        {
            var existingProducto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                .Include(p => p.IdAutorNavigation)
                .FirstOrDefaultAsync(p => p.Id == producto.Id);

            if (existingProducto == null)
                throw new Exception("El producto no se ha encontrado");

            // Actualiza solo propiedades simples
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Descripcion = producto.Descripcion;
            existingProducto.Precio = producto.Precio;
            existingProducto.Stock = producto.Stock;
            existingProducto.Estado = producto.Estado;
            existingProducto.AnioPublicacion = producto.AnioPublicacion;
            existingProducto.IdAutor = producto.IdAutor;
            existingProducto.ClasificacionEdad = producto.ClasificacionEdad;
            existingProducto.Editorial = producto.Editorial;
            existingProducto.PromedioValoracion = producto.PromedioValoracion;
            existingProducto.EsPersonalizable = producto.EsPersonalizable;

            // Actualiza el autor si corresponde
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

            // Actualiza solo categorías
            if (existingProducto.ProductoCategorias == null)
                existingProducto.ProductoCategorias = new List<ProductoCategorias>();
            existingProducto.ProductoCategorias.Clear();
            if (selectedCategorias != null && selectedCategorias.Length > 0)
            {
                var categorias = await _context.Categorias
                    .Where(c => selectedCategorias.Contains(c.Id.ToString()))
                    .ToListAsync();

                foreach (var cat in categorias)
                {
                    existingProducto.ProductoCategorias.Add(new ProductoCategorias
                    {
                        ProductoId = existingProducto.Id,
                        CategoriaId = cat.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Reseñas>> GetReseñasPorProductoAsync(int productoId)
        {
            return await _context.Reseñas
                .Where(r => r.ProductoId == productoId && r.Aprobada == true)
                .Include(r => r.Usuario)
                .ToListAsync();
        }
    }
}

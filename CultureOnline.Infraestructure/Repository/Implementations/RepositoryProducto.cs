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
                    .Include(p => p.ProductoEtiquetas)
                    .ThenInclude(pe => pe.Etiqueta)
                .Include(p => p.Promociones)
                .Include(p => p.ProductoImagenes)
                .Include(p => p.Reseñas)
                .Include(p => p.IdAutorNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);

            return producto!;
        }
        public async Task<ICollection<Productos>> ListAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.ProductoCategorias).ThenInclude(pc => pc.Categoria)
                 .Include(p => p.ProductoEtiquetas).ThenInclude(pe => pe.Etiqueta)
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
                .Include(p => p.ProductoCategorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) throw new Exception("Producto no encontrado");

            producto.ProductoCategorias.Clear(); // Limpia la relación que hay de muchos a muchos
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Productos>> GetLibroByCategoria(int idCategoria)
        {
            return await _context.Productos
                 .Where(p => p.ProductoCategorias.Any(pc => pc.CategoriaId == idCategoria))
                 .Include(p => p.ProductoCategorias).ThenInclude(pc => pc.Categoria)
                 .Include(p => p.ProductoEtiquetas).ThenInclude(pe => pe.Etiqueta)
                .ToListAsync();
        }
        public async Task UpdateAsync(Productos producto, string[] selectedCategorias)
        {
            var existingProducto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                .Include(p => p.IdAutorNavigation)
                .FirstOrDefaultAsync(p => p.Id == producto.Id);


            // Los campos se actualizan uno a la vez
            
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Descripcion = producto.Descripcion;
            existingProducto.Precio = producto.Precio;
            existingProducto.Stock = producto.Stock;
            existingProducto.Estado = producto.Estado;
            existingProducto.AnioPublicacion = producto.AnioPublicacion;
            existingProducto.IdAutor = producto.IdAutor;
            existingProducto.ClasificacionEdad = producto.ClasificacionEdad;
            existingProducto.Editorial = producto.Editorial;
       
            existingProducto.EsPersonalizable = producto.EsPersonalizable;


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

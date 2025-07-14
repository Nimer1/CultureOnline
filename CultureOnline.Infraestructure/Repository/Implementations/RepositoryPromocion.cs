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
    public class RepositoryPromocion : IRepositoryPromocion
    {
        private readonly CultureOnlineContext _context;

        public RepositoryPromocion(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Promociones>> ListAsync()
        {
<<<<<<< HEAD
            var now = DateTime.Now;

            var promociones = await _context.Promociones
                .Include(p => p.TipoPromocion)
                .Include(p => p.Producto)
                .Include(p => p.Categoria)
                .Select(p => new Promociones
                {
                    IdPromocion = p.IdPromocion,
                    Nombre = p.Nombre,
                    TipoPromocion = new TipoPromocion
                    {
                        Descripcion = p.TipoPromocion.Descripcion

                    },
                    ProductoId = p.ProductoId,
                    Producto = p.Producto == null ? null : new Productos
                    {
                        Nombre = p.Producto.Nombre
                    },
                    CategoriaId = p.CategoriaId,
                    Categoria = p.Categoria == null ? null : new Categorias
                    {
                        Id = p.Categoria.Id,
                        Nombre = p.Categoria.Nombre
                    },
                    DescuentoPorcentaje = p.DescuentoPorcentaje,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    Estado = now < p.FechaInicio
                        ? "Pendiente"
                        : (now >= p.FechaInicio && now <= p.FechaFin ? "Vigente" : "Aplicado")
                })
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();

            return promociones;
=======
            return await _context.Set<Promociones>().ToListAsync();
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
        }

        public async Task<Promociones> FindByIdAsync(int id)
        {
<<<<<<< HEAD
            var now = DateTime.Now;

            var promocion = await _context.Promociones
                .Include(p => p.TipoPromocion)
                .Include(p => p.Producto)
                .Include(p => p.Categoria)
                .Where(p => p.IdPromocion == id)
                .Select(p => new Promociones
                {
                    IdPromocion = p.IdPromocion,
                    Nombre = p.Nombre,
                    TipoPromocion = new TipoPromocion
                    {
                        Descripcion = p.TipoPromocion.Descripcion
                    },
                    Producto = p.Producto == null ? null : new Productos
                    {
                        Nombre = p.Producto.Nombre
                    },
                    Categoria = p.Categoria == null ? null : new Categorias
                    {
                        Id = p.Categoria.Id,
                        Nombre = p.Categoria.Nombre
                    },
                    DescuentoPorcentaje = p.DescuentoPorcentaje,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    Estado = now < p.FechaInicio
                        ? "Pendiente"
                        : (now >= p.FechaInicio && now <= p.FechaFin ? "Vigente" : "Aplicado")
                })
                .FirstOrDefaultAsync();

            return promocion!;
=======
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<Promociones>().FindAsync(id);

            return @object!;

        }

        Task<ICollection<Etiquetas>> IRepositoryPromocion.ListAsync()
        {
            throw new NotImplementedException();
        }

        Task<Etiquetas> IRepositoryPromocion.FindByIdAsync(int id)
        {
            throw new NotImplementedException();
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
        }
    }
}

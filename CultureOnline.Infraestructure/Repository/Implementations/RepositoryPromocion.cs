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
            // Primero se obtiene la fecha y hora actual para evaluar el estado de las promociones
            var now = DateTime.Now;

            // Consulta asíncrona para obtener las promociones con sus relaciones
            var promociones = await _context.Promociones
                .Include(p => p.TipoPromocion)
                .Include(p => p.Producto)
                .Include(p => p.Categoria)
                .Select(p => new Promociones
                {
                    // Se mapean las propiedades básicas
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

                    // Calcular el estado basado en las fechas:
                    // Pendiente si la fecha actual es anterior a FechaInicio
                    // Vigente si está entre FechaInicio y FechaFin
                    // Aplicado si la fecha actual es posterior a FechaFin
                    Estado = now < p.FechaInicio
                        ? "Pendiente"
                        : (now >= p.FechaInicio && now <= p.FechaFin ? "Vigente" : "Aplicado")
                })
                // Luego se ordenan por fecha de inicio descendente, las más recientes primero
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();

            return promociones;
        }

        public async Task<Promociones> FindByIdAsync(int id)
        {
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
                    TipoPromocionId = p.TipoPromocionId,
                    CategoriaId = p.CategoriaId,
                    ProductoId = p.ProductoId,
                    TipoPromocion = new TipoPromocion
                    {
                        IdTipoPromocion = p.TipoPromocion.IdTipoPromocion,  
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
        }

        public async Task AddAsync(Promociones promocion)
        {
            _context.Promociones.Add(promocion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Promociones promocion)
        {
            // Busca asíncronamente la entidad Promociones por su ID
            var entity = await _context.Promociones.FindAsync(id);
            // Se verifica si la entidad existe antes de proceder con la actualización
            if (entity != null)
            {
                // Actualiza cada propiedad de la entidad existente con los valores del objeto recibido
                entity.Nombre = promocion.Nombre;
                entity.TipoPromocionId = promocion.TipoPromocionId;
                entity.ProductoId = promocion.ProductoId;
                entity.CategoriaId = promocion.CategoriaId;
                entity.DescuentoPorcentaje = promocion.DescuentoPorcentaje;
                entity.FechaInicio = promocion.FechaInicio;
                entity.FechaFin = promocion.FechaFin;
                await _context.SaveChangesAsync();
            }
        }
    }
}

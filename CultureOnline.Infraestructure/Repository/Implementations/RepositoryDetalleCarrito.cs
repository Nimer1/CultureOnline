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
    public class RepositoryDetalleCarrito : IRepositoryDetalleCarrito
    {
        private readonly CultureOnlineContext _context;

        public RepositoryDetalleCarrito(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<DetalleCarrito>> ListAsync()
        {
            return await _context.Set<DetalleCarrito>().ToListAsync();
        }

        public async Task<DetalleCarrito> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<DetalleCarrito>().FindAsync(id);

            return @object!;

        }
        public Task AddAsync(DetalleCarrito detalleCarrito)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<DetalleCarrito>> FindByCarritoIdAsync(int carritoId)
        {
            throw new NotImplementedException();
        }

    }
}

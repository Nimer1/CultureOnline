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
    public class RepositoryPago : IRepositoryPago
    {
        private readonly CultureOnlineContext _context;

        public RepositoryPago(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<RepositoryPago>> ListAsync()
        {
            return await _context.Set<RepositoryPago>().ToListAsync();
        }

        public async Task<Pago> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<Pago>().FindAsync(id);

            return @object!;

        }

        Task<ICollection<Pago>> IRepositoryPago.ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Pago pago)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Pago pago)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Pago>> FindByUsuarioIdAsync(string usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Pago>> FindByProductoIdAsync(int productoId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Pago>> FindByFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }
    }
}

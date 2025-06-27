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
    public class RepositoryTipoPromocion : IRepositoryTipoPromocion
    {
        private readonly CultureOnlineContext _context;

        public RepositoryTipoPromocion(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TipoPromocion>> ListAsync()
        {
            return await _context.Set<TipoPromocion>().ToListAsync();
        }

        public async Task<TipoPromocion> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<TipoPromocion>().FindAsync(id);

            return @object!;

        }
        Task<ICollection<TipoPromocion>> IRepositoryTipoPromocion.ListAsync()
        {
            throw new NotImplementedException();
        }

        Task<TipoPromocion> IRepositoryTipoPromocion.FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TipoPromocion> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(TipoPromocion tipoPromocion)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, TipoPromocion tipoPromocion)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TipoPromocion>> GetTipoPromocionByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

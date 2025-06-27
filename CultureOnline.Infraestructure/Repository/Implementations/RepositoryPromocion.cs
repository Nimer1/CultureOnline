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
            return await _context.Set<Promociones>().ToListAsync();
        }

        public async Task<Promociones> FindByIdAsync(int id)
        {
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
        }
    }
}

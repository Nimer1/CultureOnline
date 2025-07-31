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



        public async Task<TipoPromocion> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<TipoPromocion>().FindAsync(id);

            return @object!;

        }
        public async Task<ICollection<TipoPromocion>> ListAsync()
        {
            return await _context.TipoPromocion.ToListAsync();
        }


    }
}

using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace CultureOnline.Infraestructure.Repository.Implementations
{
    public class RepositoryCategoria: IRepositoryCategoria
    {
        private readonly CultureOnlineContext _context;
        //Alt+Enter
        public RepositoryCategoria(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<Categorias> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Categorias>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Categorias>> ListAsync()
        {
            var collection = await _context.Set<Categorias>().AsNoTracking().ToListAsync();
            return collection;
        }
    }
}

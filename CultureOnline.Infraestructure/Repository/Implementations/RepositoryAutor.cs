using CultureOnline.Infraestructure.Data;
using CultureOnline.Infraestructure.Repository.Interfaces;
using CultureOnline.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CultureOnline.Infraestructure.Repository.Implementations
{
    public class RepositoryAutor : IRepositoryAutor
    {
        private readonly CultureOnlineContext _context;

        public RepositoryAutor(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Autor>> ListAsync()
        {
            return await _context.Set<Autor>().ToListAsync();
        }

        public async Task<Autor> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<Autor>().FindAsync(id);

            return @object!;

        }
    }
}


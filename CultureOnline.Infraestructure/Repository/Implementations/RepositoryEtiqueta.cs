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
    public class RepositoryEtiqueta : IRepositoryEtiqueta
    {
        private readonly CultureOnlineContext _context;

        public RepositoryEtiqueta(CultureOnlineContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Etiquetas>> ListAsync()
        {
            return await _context.Set<Etiquetas>().ToListAsync();
        }

        public async Task<Etiquetas> FindByIdAsync(int id)
        {
            //return await _context.Set<Autor>().FindAsync(id);
            var @object = await _context.Set<Etiquetas>().FindAsync(id);

            return @object!;

        }

        Task<ICollection<Etiquetas>> IRepositoryEtiqueta.ListAsync()
        {
            throw new NotImplementedException();
        }

        Task<Etiquetas> IRepositoryEtiqueta.FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Etiquetas>> FindByProductoIdAsync(int productoId)
        {
            throw new NotImplementedException();
        }
    }
}

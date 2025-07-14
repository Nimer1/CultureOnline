using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryProductoCategorias
    {
        Task<ICollection<ProductoCategorias>> ListAsync();
        Task<IEnumerable<ProductoCategorias>> GetAllAsync();
        Task<ProductoCategorias?> GetByIdsAsync(int productoId, int categoriaId);
        Task AddAsync(ProductoCategorias entity);
        Task DeleteAsync(int productoId, int categoriaId);
    }
}

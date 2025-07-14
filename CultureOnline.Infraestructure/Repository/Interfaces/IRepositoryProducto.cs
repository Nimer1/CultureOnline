using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryProducto
    {
        Task<ICollection<Productos>> ListAsync();
        Task<Productos> FindByIdAsync(int id);
        Task<int> AddAsync(Productos objectMapped, string[] selectedCategorias);
        Task DeleteAsync(int id);
        Task UpdateAsync(Productos entity, string[] selectedCategorias);
        Task<List<Reseñas>> GetReseñasPorProductoAsync(int productoId);

        //Task<ICollection<Productos>> GetProductoByCategoria(int idCategoria);
    }
}

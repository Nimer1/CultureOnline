using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPedido
    { 
        Task<ICollection<Pedidos>> ListAsync();
        Task<Pedidos> FindByIdAsync(int id);


        Task<Pedidos> FindByIdChangeAsync(int id);

        Task<int> AddAsync(Pedidos entity);

        Task<int> GetNextNumberOrden();

        Task<ICollection<Pedidos>> OrdenByClientId(string id);

    }
}

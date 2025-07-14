using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryDetalleCarrito
    {
        Task<ICollection<DetalleCarrito>> ListAsync();
        Task<DetalleCarrito> FindByIdAsync(int id);
        Task<ICollection<DetalleCarrito>> FindByCarritoIdAsync(int carritoId);
        Task AddAsync(DetalleCarrito detalleCarrito);

    }
}

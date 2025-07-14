using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CultureOnline.Infraestructure.Models;
namespace CultureOnline.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryPago
    {
        Task<ICollection<Pago>> ListAsync();
        Task<Pago> FindByIdAsync(int id);

        Task AddAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
        Task<ICollection<Pago>> FindByUsuarioIdAsync(string usuarioId);
        Task<ICollection<Pago>> FindByProductoIdAsync(int productoId);
        Task<ICollection<Pago>> FindByFechaAsync(DateTime fechaInicio, DateTime fechaFin);


    }
}

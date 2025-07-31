using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IservicePedido
    {
        Task<ICollection<PedidoDTO>> ListAsync();
        Task<PedidoDTO> FindByIdAsync(int id);

        Task<PedidoDTO> FindByIdChangeAsync(int id);
        Task<int> AddAsync(PedidoDTO dto);
        Task<int> GetNextNumberOrden();
    }
}

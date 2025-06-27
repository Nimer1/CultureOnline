using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    internal interface IServiceDetalleCarrito
    {
        Task<ICollection<DetalleCarritoDTO>> ListAsync();
        Task<DetalleCarritoDTO> FindByIdAsync(int id);
        Task<DetalleCarritoDTO> FindByIdChangeAsync(int id);
        Task<int> AddAsync(DetalleCarritoDTO dto);
        Task<int> GetNextNumberOrden();
    }
}


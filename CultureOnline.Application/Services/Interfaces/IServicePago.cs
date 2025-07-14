using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServicePago
    {
        Task<ICollection<PagoDTO>> ListAsync();
        Task<PagoDTO> FindByIdAsync(int id);
    }
}

using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServiceTipoPromocion
    {
        Task<ICollection<TipoPromocionDTO>> ListAsync();
        Task<TipoPromocionDTO> FindByIdAsync(int id);
    }
}

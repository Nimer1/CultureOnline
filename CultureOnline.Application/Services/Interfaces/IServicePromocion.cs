using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServicePromocion
    {
        Task<PromocionDTO> FindByIdAsync(int id);
       
        Task<ICollection<PromocionDTO>> ListAsync();
        Task AddAsync(PromocionDTO dto);
        Task UpdateAsync(int id, PromocionDTO dto);
    }
}

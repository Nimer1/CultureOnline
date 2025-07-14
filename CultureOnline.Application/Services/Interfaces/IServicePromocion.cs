using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
<<<<<<< HEAD
    public interface IServicePromocion
    {
        Task<PromocionDTO> FindByIdAsync(int id);
=======
    internal interface IServicePromocion
    {
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651
        Task<ICollection<PromocionDTO>> FindByNameAsync(string nombre);
        Task<ICollection<PromocionDTO>> ListAsync();
    }
}

using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServiceReseña
    {
        Task<ICollection<ReseñaDTO>> ListAsync();
        Task<ReseñaDTO> FindByIdAsync(int id);
<<<<<<< HEAD
        Task AddAsync(ReseñaDTO reseña);
        Task UpdateAsync(int id, ReseñaDTO reseña);
        Task DeleteAsync(int id);
=======
>>>>>>> 22ca98ce21393483d4490652a8d3de1a5a180651

    }
}

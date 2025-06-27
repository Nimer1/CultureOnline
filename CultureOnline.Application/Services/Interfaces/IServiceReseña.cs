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

    }
}

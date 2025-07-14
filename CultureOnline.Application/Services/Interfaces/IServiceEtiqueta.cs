using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    internal interface IServiceEtiqueta
    {
        Task<ICollection<EtiquetaDTO>> ListAsync();
        Task<EtiquetaDTO> FindByIdAsync(int id);
    }
}

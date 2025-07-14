using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    internal interface IServiceProductoEtiqueta
    {
        Task<ICollection<ProductoEtiquetasDTO>> ListAsync();
        Task<ProductoEtiquetasDTO> FindByIdAsync(int id);
    }
}

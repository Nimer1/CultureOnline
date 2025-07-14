using CultureOnline.Application.DTOs;
using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    internal interface IServiceProductoImagenes
    {
        Task<ICollection<ProductoImagenesDTO>> ListAsync();
        Task<ProductoImagenesDTO> FindByIdAsync(int id);
    }
}

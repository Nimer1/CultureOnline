using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServiceProducto
    {
        Task<ICollection<ProductoDTO>> ListAsync();
        Task<ProductoDTO> FindByIdAsync(int id);
        Task<List<ReseñaDTO>> GetReseñasPorProductoAsync(int productoId);
        Task UpdateAsync(int id, ProductoDTO dto, string[] selectedCategorias); // Agregado para permitir actualización de producto
        //Task<ICollection<ProductoDTO>> GetProductoByCategoria(int categoriaId);

    }
}

using CultureOnline.Application.DTOs;

namespace CultureOnline.Application.Services.Interfaces
{
    public interface IServiceProductoCategorias
    {
        Task<ICollection<ProductoCategoriasDTO>> ListAsync();
        Task<IEnumerable<ProductoCategoriasDTO>> GetAllAsync();
        Task<ProductoCategoriasDTO?> GetByIdsAsync(int productoId, int categoriaId);
        Task AddAsync(ProductoCategoriasDTO dto);
        Task DeleteAsync(int productoId, int categoriaId);
    }
}

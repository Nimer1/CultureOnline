using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Implementations;
using CultureOnline.Infraestructure.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace CultureOnline.Application.Services.Implementations
{
    public class ServiceProductoCategorias : IServiceProductoCategorias
    {
        private readonly IRepositoryProductoCategorias _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProductoCategorias> _logger;
        public ServiceProductoCategorias(IRepositoryProductoCategorias repo, IMapper mapper, ILogger<ServiceProductoCategorias> logger)
        {
            _repository = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ICollection<ProductoCategoriasDTO>> ListAsync()
        {
            //Obtener datos del repositorio 
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO> 
            var collection = _mapper.Map<ICollection<ProductoCategoriasDTO>>(list);
            // Return lista 
            return collection;
        }

        public async Task<IEnumerable<ProductoCategoriasDTO>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductoCategoriasDTO>>(list);
        }

        public async Task<ProductoCategoriasDTO?> GetByIdsAsync(int productoId, int categoriaId)
        {
            var entity = await _repository.GetByIdsAsync(productoId, categoriaId);
            return entity == null ? null : _mapper.Map<ProductoCategoriasDTO>(entity);
        }

        public async Task AddAsync(ProductoCategoriasDTO dto)
        {
            var entity = _mapper.Map<ProductoCategorias>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int productoId, int categoriaId)
        {
            await _repository.DeleteAsync(productoId, categoriaId);
        }
    }
}

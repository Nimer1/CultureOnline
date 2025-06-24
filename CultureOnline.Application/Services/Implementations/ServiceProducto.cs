using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Application.Services.Interfaces;
using CultureOnline.Infraestructure.Models;
using CultureOnline.Infraestructure.Repository.Interfaces;

namespace CultureOnline.Application.Services.Implementations
{
    public  class ServiceProducto : IServiceProducto
    {
        private readonly IRepositoryProducto _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProducto> _logger;

        public ServiceProducto(IRepositoryProducto repository, IMapper mapper, ILogger<ServiceProducto> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> AddAsync(ProductoDTO dto, string[] selectedCategorias)
        {
            // Map LibroDTO to Libro
            var objectMapped = _mapper.Map<Productos>(dto);

            // Return
            return await _repository.AddAsync(objectMapped, selectedCategorias);
        }
        //Listar productos
        public async Task<ProductoDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<ProductoDTO>(@object);
            return objectMapped;
        }

        //Borrar un producto por ID
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        //
        public async Task<ICollection<ProductoDTO>> ListAsync()
        {
            // Get data from Repository
            var list = await _repository.ListAsync();
            // Map List<Producto> to ICollection<ProductoDTO>
            var collection = _mapper.Map<ICollection<ProductoDTO>>(list);
            // Return Data
            return collection;
        }
        // Actualizar un producto por ID
        public async Task UpdateAsync(int id, ProductoDTO dto, string[] selectedCategorias)
        {
            //Obtenga el modelo original a actualizar
            var @object = await _repository.FindByIdAsync(id);
            //       source, destination
            var entity = _mapper.Map(dto, @object!);


            await _repository.UpdateAsync(entity, selectedCategorias);
        }
        // Obtener productos por categoría
        /*public async Task<ICollection<ProductoDTO>> GetProductoByCategoria(int IdCategoria)
        {
            // Get data from Repository
            var list = await _repository.GetProductoByCategoria(IdCategoria);
            // Map List<Libro> to ICollection<LibroDTO>
            var collection = _mapper.Map<ICollection<ProductoDTO>>(list);
            // Return Data
            return collection;
        }*/

    }
}

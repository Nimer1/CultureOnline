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
    public class ServiceProducto : IServiceProducto
    {
        private readonly IRepositoryProducto _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceProducto> _logger;
        private readonly IServicePromocion _servicePromocion;
        private readonly IServiceProductoCategorias _serviceProductoCategorias;
        public ServiceProducto(IRepositoryProducto repository, IMapper mapper, ILogger<ServiceProducto> logger,
                                        IServicePromocion servicePromocion, IServiceProductoCategorias serviceProductoCategorias)
        {
            _servicePromocion = servicePromocion;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _serviceProductoCategorias = serviceProductoCategorias;
        }
        public async Task<int> AddAsync(ProductoDTO dto, string[] selectedCategorias)
        {
            // Map ProductoDTO to Producto
            var objectMapped = _mapper.Map<Productos>(dto);

            // Return
            return await _repository.AddAsync(objectMapped, selectedCategorias);
        }

        //Se listan los productos
        public async Task<ProductoDTO> FindByIdAsync(int id)
        {
            var producto = await _repository.FindByIdAsync(id);
            if (producto == null)
                return null;
            var productoDTO = _mapper.Map<ProductoDTO>(producto);
            if (productoDTO.Promociones == null)
                productoDTO.Promociones = new List<PromocionDTO>();
            if (productoDTO.Categorias == null)
                productoDTO.Categorias = new List<CategoriaDTO>();

            // Esto de aqui trae todas las promociones vigentes
            var promociones = await _servicePromocion.ListAsync();

            // Promociones directas al producto
            var promocionesDirectas = productoDTO.Promociones
                .Where(p =>
                    p.Estado == "Vigente" &&
                    p.ProductoId == productoDTO.Id &&
                    p.FechaInicio <= DateTime.Now &&
                    p.FechaFin >= DateTime.Now)
                .ToList();

            // Promociones por categoría
            var promocionesPorCategoria = promociones
                .Where(p =>
                    p.Estado == "Vigente" &&
                    p.CategoriaId.HasValue &&
                    productoDTO.Categorias.Any(c => c.Id == p.CategoriaId.Value) &&
                    p.FechaInicio <= DateTime.Now &&
                    p.FechaFin >= DateTime.Now)
                .ToList();

            // Aquí une todas las promociones y suma los descuentos
            var todasPromociones = promocionesDirectas.Concat(promocionesPorCategoria).Distinct().ToList();

            int descuentoTotal = todasPromociones.Sum(p => p.DescuentoPorcentaje);
            if (descuentoTotal > 80)
                descuentoTotal = 80;

            if (descuentoTotal > 0)
            {
                productoDTO.PrecioPromocional = productoDTO.Precio - (productoDTO.Precio * descuentoTotal / 100m);
                productoDTO.DescuentoPorcentajePromocion = descuentoTotal;
                productoDTO.NombresPromociones = todasPromociones.Select(p => p.Nombre).ToList();
            }
            else
            {
                productoDTO.PrecioPromocional = null;
                productoDTO.DescuentoPorcentajePromocion = null;
                productoDTO.NombresPromociones = new List<string>();
            }
            return productoDTO;
        }

        //Borrar un producto por ID
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        // Listar todos los productos
        public async Task<ICollection<ProductoDTO>> ListAsync()
        {
            var productos = await _repository.ListAsync();
            var productosDTO = _mapper.Map<ICollection<ProductoDTO>>(productos);
            var promociones = await _servicePromocion.ListAsync();
            var categorias = await _serviceProductoCategorias.ListAsync();

            foreach (var prod in productosDTO)
            {
                // Promociones directas
                var promocionesDirectas = prod.Promociones
                    .Where(p =>
                        p.Estado == "Vigente" &&
                        p.ProductoId == prod.Id &&
                        p.FechaInicio <= DateTime.Now &&
                        p.FechaFin >= DateTime.Now)
                    .ToList();

                // Promociones por categoría
                var promocionesPorCategoria = promociones
                    .Where(p =>
                        p.Estado == "Vigente" &&
                        p.CategoriaId.HasValue &&
                        prod.Categorias.Any(c => c.Id == p.CategoriaId.Value) &&
                        p.FechaInicio <= DateTime.Now &&
                        p.FechaFin >= DateTime.Now)
                    .ToList();

                var todasPromociones = promocionesDirectas.Concat(promocionesPorCategoria).Distinct().ToList();

                int descuentoTotal = todasPromociones.Sum(p => p.DescuentoPorcentaje);
                if (descuentoTotal > 100)
                    descuentoTotal = 100;

                if (descuentoTotal > 0)
                {
                    prod.PrecioPromocional = prod.Precio - (prod.Precio * descuentoTotal / 100m);
                    prod.DescuentoPorcentajePromocion = descuentoTotal;
                    prod.NombresPromociones = todasPromociones.Select(p => p.Nombre).ToList();
                }
                else
                {
                    prod.PrecioPromocional = null;
                    prod.DescuentoPorcentajePromocion = null;
                    prod.NombresPromociones = new List<string>();
                }
            }
            return productosDTO;
        }


        // Actualiza un producto por ID
        public async Task UpdateAsync(int id, ProductoDTO dto, string[] selectedCategorias)
        {
            //Se obtiene el modelo original a actualizar
            var @object = await _repository.FindByIdAsync(id);
            var entity = _mapper.Map(dto, @object!);


            await _repository.UpdateAsync(entity, selectedCategorias);
        }

        public async Task<List<ReseñaDTO>> GetReseñasPorProductoAsync(int productoId)
        {
            var reseñas = await _repository.GetReseñasPorProductoAsync(productoId);
            return _mapper.Map<List<ReseñaDTO>>(reseñas);
        }
    }
}


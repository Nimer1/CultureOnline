using System.Linq;
using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Infraestructure.Models;

namespace CultureOnline.Application.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            // Productos <-> ProductoDTO (incluye imágenes y categorías)
            CreateMap<Productos, ProductoDTO>()
                .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.ProductoImagenes))
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(src => src.ProductoCategorias.Select(pc => pc.Categoria)))
                .ReverseMap();

            CreateMap<ProductoDTO, Productos>()
                .ForMember(dest => dest.ProductoImagenes, opt => opt.Ignore())
                .ForMember(dest => dest.Promociones, opt => opt.Ignore())
                .ForMember(dest => dest.Reseñas, opt => opt.Ignore())
                .ForMember(dest => dest.ProductoCategorias, opt => opt.Ignore())
                .ForMember(dest => dest.Etiquetas, opt => opt.Ignore())
                .ForMember(dest => dest.Categorias, opt => opt.Ignore());
            CreateMap<ProductoImagenes, ProductoImagenesDTO>().ReverseMap();
            CreateMap<Categorias, CategoriaDTO>().ReverseMap();

            // Promociones <-> PromocionDTO (incluye tipo de promoción y categoría)
            CreateMap<Promociones, PromocionDTO>()
                .ForMember(dest => dest.TipoPromocion, opt => opt.MapFrom(src => src.TipoPromocion))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
                .ReverseMap();

            CreateMap<TipoPromocion, TipoPromocionDTO>().ReverseMap();
        }
    }
}
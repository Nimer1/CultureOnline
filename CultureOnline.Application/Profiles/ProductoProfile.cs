using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Infraestructure.Models;
namespace CultureOnline.Application.Profiles
{
    public class ProductoProfile: Profile
    {
        public ProductoProfile() {
            CreateMap<Productos, ProductoDTO>()
                   .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.ProductoImagenes))
                   .ReverseMap();

            CreateMap<ProductoImagenes, ProductoImagenesDTO>().ReverseMap();
        }
    }
    
}

using AutoMapper;
using CultureOnline.Application.DTOs;
using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.Profiles
{
    public class PromocionProfile : Profile
    {
        public PromocionProfile()
        {

          CreateMap<Promociones, PromocionDTO>()
         .ForMember(dest => dest.TipoPromocionId, opt => opt.MapFrom(src => src.TipoPromocionId))
         .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
         .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId))
         .ReverseMap();
        }
    }
}

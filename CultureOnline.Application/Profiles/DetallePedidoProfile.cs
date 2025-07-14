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
    public class DetallePedidoProfile : Profile
    {
        public DetallePedidoProfile()
        {
            CreateMap<DetallePedido, DetallePedidoDTO>()
                .ForMember(dest => dest.Producto, opt => opt.MapFrom(src => src.Producto))
                .ReverseMap();
        }
    }
}

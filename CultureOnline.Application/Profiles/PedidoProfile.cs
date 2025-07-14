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
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedidos, PedidoDTO>()
                .ForMember(dest => dest.DetallePedido, opt => opt.MapFrom(src => src.DetallePedido))
                .ReverseMap();
        }
    }
}

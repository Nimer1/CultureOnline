using CultureOnline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CultureOnline.Infraestructure.Models;

namespace CultureOnline.Application.Profiles
{
    public class ProductoCategoriasProfile :Profile
    {
        public ProductoCategoriasProfile()
        {
            // Mapeo entre ProductoCategorias y ProductoCategoriasDTO
            // ProductoCategoriasDTO incluye información de la categoría
            CreateMap<ProductoCategorias, ProductoCategoriasDTO>().ReverseMap();
        }
    }
}

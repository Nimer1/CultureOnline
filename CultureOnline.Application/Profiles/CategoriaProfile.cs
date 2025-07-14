using CultureOnline.Application.DTOs;
using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace CultureOnline.Application.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {

            CreateMap<CategoriaDTO, Categorias>().ReverseMap();
        }
    }
}

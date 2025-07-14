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
    public class AutorProfile : Profile
    {
        public AutorProfile()
        {

            CreateMap<AutorDTO, Autor>().ReverseMap();
        }
    }
}

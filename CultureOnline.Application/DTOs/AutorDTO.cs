using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record AutorDTO
    {
        [Display(Name = "Id")]
        public int IdAutor { get; set; }

        [Display(Name = "Nombre Autor")]
        public string Descripcion { get; set; } = null!;

       public List<ProductoDTO>? Productos{ get; set; } = null;

    }
}

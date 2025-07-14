using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record CategoriaDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Categoría")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Estado")]
        public string? Estado { get; set; }
        public List<ProductoDTO>? Productos { get; set; }
        public List<PromocionDTO>? Promociones { get; set; }
    }
}

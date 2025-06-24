using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record GeneroProductoDTO
    {
        [Display(Name = "ID Género")]
        public int IdGenero { get; set; }

        [Display(Name = "Descripción del Género")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; } = null!;

 
        //public List<ProductoDTO> Productos { get; set; } = new();
    }
}


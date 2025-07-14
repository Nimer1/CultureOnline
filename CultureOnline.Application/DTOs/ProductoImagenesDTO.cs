using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record ProductoImagenesDTO
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        [Display(Name = "Ruta de la imagen")]
        public string? Ruta { get; set; }

        public ProductoDTO? Producto { get; set; }
    }
}

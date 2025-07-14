using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record DetalleCarritoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Carrito")]
        public int? CarritoId { get; set; }

        [Display(Name = "Producto")]
        public int? ProductoId { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Cantidad { get; set; }

        public CarritoDTO? Carrito { get; set; }
        public ProductoDTO? Producto { get; set; }
    }
}

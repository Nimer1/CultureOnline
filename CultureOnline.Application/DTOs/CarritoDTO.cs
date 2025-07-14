using CultureOnline.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record CarritoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int UsuarioId { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime FechaModificacion { get; set; }

        public UsuarioDTO Usuario { get; set; } = null!;

        public List<DetalleCarritoDTO> DetalleCarrito { get; set; } = new();
    }
}

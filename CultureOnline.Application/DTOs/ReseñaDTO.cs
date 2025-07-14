using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record ReseñaDTO
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int UsuarioId { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int ProductoId { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Comentario { get; set; }

        [Display(Name = "Pedido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int PedidoId { get; set; }

        [Display(Name = "Valoración")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Valoracion { get; set; }

        [Display(Name = "¿Aprobada?")]
        public bool? Aprobada { get; set; }

        public PedidoDTO? Pedido { get; set; }
        public ProductoDTO? Producto { get; set; }
        public UsuarioDTO? Usuario { get; set; }

    }
}

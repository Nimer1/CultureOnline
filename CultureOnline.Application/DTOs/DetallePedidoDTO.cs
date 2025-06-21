using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record DetallePedidoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Pedido")]
        public int? PedidoId { get; set; }

        [Display(Name = "Producto")]
        public int? ProductoId { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio unitario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal SubTotal { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Impuesto { get; set; }

        [Display(Name = "Observación")]
        public string? Observacion { get; set; }

        public PedidoDTO? Pedido { get; set; }

        public ProductoDTO? Producto { get; set; }
    }
}

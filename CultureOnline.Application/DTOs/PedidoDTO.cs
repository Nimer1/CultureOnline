using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record PedidoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int? UsuarioId { get; set; }

        [Display(Name = "Fecha del pedido")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Dirección de envío")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string? Direccion { get; set; }

        [Display(Name = "Estado")]
        public string? Estado { get; set; } = null!;

        /*[Display(Name = "Método de Pago")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdmetodoPago { get; set; }*/

        [Display(Name = "Código de Descuento")]
        public string? CodigoDescuento { get; set; }

        [Display(Name = "Notas")]
        public string? Notas { get; set; }

        [Display(Name = "Seguimiento del pedido")]
        public string? IdSeguimiento { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Imagen Personalizada")]
        public string? ImagenPersonalizada { get; set; }

        [Display(Name = "Número de Factura")]
        public string? NumeroFactura { get; set; }

        public virtual UsuarioDTO? Usuario { get; set; }

        public virtual List<DetallePedidoDTO> DetallePedido { get; set; } = new();

        public virtual List<PagoDTO> Pago { get; set; } = new();

        public virtual List<ReseñaDTO> Reseñas { get; set; } = new();
        public virtual List<ProductoPersonalizadoDTO> ProductosPersonalizados { get; set; } = new();

    }
}

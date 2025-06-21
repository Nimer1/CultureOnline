using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record PagoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Pedido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int PedidoId { get; set; }

        [Display(Name = "Nombre Titular")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string NombreTitular { get; set; } = null!;

        [Display(Name = "Número de tarjeta")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Numero { get; set; } = null!;

        [Display(Name = "Fecha de Expiración")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string FechaExpiracion { get; set; } = null!;

        [Display(Name = "Código CVV")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string? CodigoAutorizacion { get; set; }

        [Display(Name = "Fecha de Pago")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Monto")]
        public decimal? Monto { get; set; }

        [Display(Name = "Descripción del pago")]
        public string? Descripcion { get; set; }

        
        public PedidoDTO? Pedido { get; set; }
    }
}

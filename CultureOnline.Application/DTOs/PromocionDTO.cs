using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record PromocionDTO
    {
        [Display(Name = "ID Promoción")]
        public int IdPromocion { get; set; }

        [Display(Name = "Nombre Promoción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Tipo de promoción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int TipoPromocionId { get; set; }

        [Display(Name = "Producto Asociado")]
        public int ProductoId { get; set; }

        [Display(Name = "Categoria Asociada")]
        public int CategoriaId { get; set; }

        [Display(Name = "Porcentaje de descuento")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(0, 100, ErrorMessage = "El porcentaje de descuento debe ser un valor entre 0 y 100")]
        public decimal DescuentoPorcentaje { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Estado")]
        public string? IdEstado { get; set; }

        public List<CategoriaDTO> Categorias { get; set; } = new();
        public ProductoDTO? Producto { get; set; }
        public TipoPromocionDTO TipoPromocion { get; set; } = null!;
    }
}

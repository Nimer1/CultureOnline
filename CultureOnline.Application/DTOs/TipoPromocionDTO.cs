using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record TipoPromocionDTO
    {
        [Display(Name = "ID Tipo de Promoción")]
        public int IdTipoPromocion { get; set; }

        [Display(Name = "Tipo de Promoción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string? Descripcion { get; set; }

        public List<PromocionDTO> Promociones { get; set; } = new();
    }
}

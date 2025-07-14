using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record ProductoEtiquetasDTO
    {
        [Display(Name = "ID del Producto")]
        public int ProductoId { get; set; }

        [Display(Name = "ID de la Etiqueta")]
        public int EtiquetaId { get; set; }

        
        public ProductoDTO? Producto { get; set; }

        public EtiquetaDTO? Etiqueta { get; set; }
    }
}

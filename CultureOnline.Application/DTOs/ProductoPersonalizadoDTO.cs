using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record ProductoPersonalizadoDTO
    {
        public int Id { get; set; }
        public ProductoDTO? Producto { get; set; }

        public int Cantidad { get; set; }

        public string? TextoPersonalizado { get; set; }
        public decimal CostoTexto { get; set; }

        public string? ImagenPersonalizada { get; set; }
        public decimal CostoImagen { get; set; }

        public string? MaterialPersonalizado { get; set; }
        public decimal CostoMaterial { get; set; }

        public decimal PrecioUnitario
        {
            get
            {
                decimal costoBase = Producto?.Precio ?? 0;
                return costoBase + CostoTexto + CostoImagen + CostoMaterial;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record CategoriaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? IdEstado { get; set; }
       // public virtual List<ProductoDTO> Productos { get; set; } = null;
    }
}

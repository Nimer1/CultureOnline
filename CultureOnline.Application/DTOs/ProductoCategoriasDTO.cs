using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
   public record ProductoCategoriasDTO
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
    }
}

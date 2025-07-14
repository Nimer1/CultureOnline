using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Models
{
     public partial class ProductoCategorias
    {

        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }

        // Relaciones de navegación
        public virtual Productos Producto { get; set; } = null!;
        public virtual Categorias Categoria { get; set; } = null!;
    }
}

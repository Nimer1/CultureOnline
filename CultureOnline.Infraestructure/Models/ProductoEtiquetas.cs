using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Models
{
    public partial class ProductoEtiquetas
    {
        public int ProductoId { get; set; }
        public int EtiquetaId { get; set; }

        public virtual Productos Producto { get; set; } = null!;
        public virtual Etiquetas Etiqueta { get; set; } = null!;
    }
}

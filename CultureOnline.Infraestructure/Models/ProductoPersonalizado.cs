using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Infraestructure.Models
{
    public partial class ProductoPersonalizado
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public virtual Pedidos? Pedido { get; set; }

        public int ProductoId { get; set; }
        public virtual Productos? Producto { get; set; }

        public int Cantidad { get; set; }

        public string? TextoPersonalizado { get; set; }
        public decimal CostoTexto { get; set; }

        public string? ImagenPersonalizada { get; set; }
        public decimal CostoImagen { get; set; }

        public string? MaterialPersonalizado { get; set; }
        public decimal CostoMaterial { get; set; }
    }
}

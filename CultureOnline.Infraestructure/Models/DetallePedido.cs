using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class DetallePedido
{
    public int Id { get; set; }

    public int? PedidoId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Impuesto { get; set; }

    public string? Observacion { get; set; }

    public virtual Pedidos? Pedido { get; set; }

    public virtual Productos? Producto { get; set; }
}

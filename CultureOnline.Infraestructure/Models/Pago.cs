using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public string NombreTitular { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string FechaExpiracion { get; set; } = null!;

    public string? CodigoAutorizacion { get; set; }

    public DateTime FechaPago { get; set; }

    public decimal? Monto { get; set; }

    public string? Descripcion { get; set; }
    public string? MetodoPago { get; set; }

    public virtual Pedidos Pedido { get; set; } = null!;
}

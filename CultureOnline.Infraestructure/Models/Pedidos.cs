using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Pedidos
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime Fecha { get; set; }

    public string? Direccion { get; set; }

    public string Estado { get; set; } = null!;

    //public int IdmetodoPago { get; set; }

    public string? CodigoDescuento { get; set; }

    public string? Notas { get; set; }

    public string? IdSeguimiento { get; set; }

    public decimal Total { get; set; }

    public string? ImagenPersonalizada { get; set; }

    public string? NumeroFactura { get; set; }

    public virtual ICollection<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();

    public virtual ICollection<Reseñas> Reseñas { get; set; } = new List<Reseñas>();

    public virtual Usuario? Usuario { get; set; }
    public virtual ICollection<ProductoPersonalizado> ProductosPersonalizados { get; set; } = new List<ProductoPersonalizado>();

}

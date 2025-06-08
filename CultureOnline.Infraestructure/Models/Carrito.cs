using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaModificacion { get; set; }

    public virtual ICollection<DetalleCarrito> DetalleCarrito { get; set; } = new List<DetalleCarrito>();

    public virtual Usuario Usuario { get; set; } = null!;
}

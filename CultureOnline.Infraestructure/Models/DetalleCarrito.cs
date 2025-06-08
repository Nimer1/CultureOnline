using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class DetalleCarrito
{
    public int Id { get; set; }

    public int? CarritoId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public virtual Carrito? Carrito { get; set; }

    public virtual Productos? Producto { get; set; }
}

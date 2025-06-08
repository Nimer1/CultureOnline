using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class ProductoImagenes
{
    public int Id { get; set; }

    public int? ProductoId { get; set; }

    public string? Ruta { get; set; }

    public virtual Productos? Producto { get; set; }
}

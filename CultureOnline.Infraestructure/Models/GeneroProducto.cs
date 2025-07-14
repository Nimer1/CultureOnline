using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class GeneroProducto
{
    public int IdGenero { get; set; }

    public string Descripcion { get; set; } = null!;

    //public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}

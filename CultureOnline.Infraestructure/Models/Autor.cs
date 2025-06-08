using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}

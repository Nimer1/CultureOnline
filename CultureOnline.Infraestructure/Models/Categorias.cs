using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Categorias
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string?  Estado { get; set; }

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();

    public virtual ICollection<Promociones> Promociones { get; set; } = new List<Promociones>();
}

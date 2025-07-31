using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Etiquetas
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    //public virtual ICollection<Productos> Producto { get; set; } = new List<Productos>();
    public virtual ICollection<ProductoEtiquetas> ProductoEtiquetas { get; set; } = new List<ProductoEtiquetas>();

}

using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class RolUsuario
{
    public int Idrol { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}

using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class TipoPromocion
{
    public int IdTipoPromocion { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Promociones> Promociones { get; set; } = new List<Promociones>();
}

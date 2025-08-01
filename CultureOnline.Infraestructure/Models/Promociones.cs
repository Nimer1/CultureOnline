﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureOnline.Infraestructure.Models;

public partial class Promociones
{
    public int IdPromocion { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoPromocionId { get; set; }

    public int? ProductoId { get; set; }

    public int? CategoriaId { get; set; }

    public decimal DescuentoPorcentaje { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }
    [NotMapped]
    public string? Estado { get; set; }

    public virtual Categorias? Categoria { get; set; }

    public virtual Productos? Producto { get; set; }

    public virtual TipoPromocion TipoPromocion { get; set; } = null!;
}

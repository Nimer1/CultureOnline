using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Reseñas
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Comentario { get; set; }

    public int PedidoId { get; set; }

    public int? Valoracion { get; set; }

    public bool? Aprobada { get; set; }

    public virtual Pedidos Pedido { get; set; } = null!;

    public virtual Productos? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}

using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int Idrol { get; set; }

    public DateTime UltimoInicioSesion { get; set; }

    public string? IdEstado { get; set; }

    public virtual ICollection<Carrito> Carrito { get; set; } = new List<Carrito>();

    public virtual RolUsuario IdrolNavigation { get; set; } = null!;

    public virtual ICollection<Pedidos> Pedidos { get; set; } = new List<Pedidos>();

    public virtual ICollection<Reseñas> Reseñas { get; set; } = new List<Reseñas>();
}

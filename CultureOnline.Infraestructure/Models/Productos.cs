using System;
using System.Collections.Generic;

namespace CultureOnline.Infraestructure.Models;

public partial class Productos
{
    public int Id { get; set; }

    public string Nombre { get; set; } =  null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public string? Estado { get; set; }

    public int? AnioPublicacion { get; set; }

    public int? IdAutor { get; set; }

    public string? ClasificacionEdad { get; set; }

    public string? Editorial { get; set; }

    //public int? IdGeneroProducto { get; set; }

    //public int CategoriaId { get; set; }

    public double? PromedioValoracion { get; set; }

    public bool EsPersonalizable { get; set; }

    //public virtual Categorias Categoria { get; set; } = null!;

    public virtual ICollection<DetalleCarrito> DetalleCarrito { get; set; } = new List<DetalleCarrito>();

    public virtual ICollection<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();

    public virtual Autor? IdAutorNavigation { get; set; }

   // public virtual GeneroProducto? IdGeneroProductoNavigation { get; set; }

    public virtual ICollection<ProductoImagenes> ProductoImagenes { get; set; } = new List<ProductoImagenes>();

    public virtual ICollection<Promociones> Promociones { get; set; } = new List<Promociones>();

    public virtual ICollection<Reseñas> Reseñas { get; set; } = new List<Reseñas>();

    public virtual ICollection<Etiquetas> Etiquetas { get; set; } = new List<Etiquetas>();
    //public virtual ICollection<ProductoEtiquetas> ProductoEtiquetas { get; set; } = new List<ProductoEtiquetas>();
    public virtual ICollection<Categorias> Categorias { get; set; } = new List<Categorias>();
    public virtual ICollection<ProductoCategorias> ProductoCategorias { get; set; } = new List<ProductoCategorias>();

}

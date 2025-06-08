using System;
using System.Collections.Generic;
using CultureOnline.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CultureOnline.Infraestructure.Data;

public partial class CultureOnlineContext : DbContext
{
    public CultureOnlineContext(DbContextOptions<CultureOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autor { get; set; }

    public virtual DbSet<Carrito> Carrito { get; set; }

    public virtual DbSet<Categorias> Categorias { get; set; }

    public virtual DbSet<DetalleCarrito> DetalleCarrito { get; set; }

    public virtual DbSet<DetallePedido> DetallePedido { get; set; }

    public virtual DbSet<Etiquetas> Etiquetas { get; set; }

    public virtual DbSet<GeneroProducto> GeneroProducto { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Pedidos> Pedidos { get; set; }

    public virtual DbSet<ProductoImagenes> ProductoImagenes { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Promociones> Promociones { get; set; }

    public virtual DbSet<Reseñas> Reseñas { get; set; }

    public virtual DbSet<RolUsuario> RolUsuario { get; set; }

    public virtual DbSet<TipoPromocion> TipoPromocion { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor);

            entity.Property(e => e.IdAutor)
                .ValueGeneratedNever()
                .HasColumnName("idAutor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carrito)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrito_Usuario");
        });

        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.Property(e => e.IdEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("idEstado");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<DetalleCarrito>(entity =>
        {
            entity.HasOne(d => d.Carrito).WithMany(p => p.DetalleCarrito)
                .HasForeignKey(d => d.CarritoId)
                .HasConstraintName("FK_DetalleCarrito_Carrito");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleCarrito)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_DetalleCarrito_Producto");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.Property(e => e.Impuesto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Observacion).HasMaxLength(100);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedido)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("FK_DetallePedido_Pedido");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallePedido)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_DetallePedido_Producto");
        });

        modelBuilder.Entity<Etiquetas>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<GeneroProducto>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK_Genero");

            entity.Property(e => e.IdGenero)
                .ValueGeneratedNever()
                .HasColumnName("idGenero");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.Property(e => e.CodigoAutorizacion).HasMaxLength(50);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaExpiracion)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("monto");
            entity.Property(e => e.NombreTitular).HasMaxLength(100);
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Pedido).WithMany(p => p.Pago)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Pedidos");
        });

        modelBuilder.Entity<Pedidos>(entity =>
        {
            entity.Property(e => e.CodigoDescuento).HasMaxLength(30);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("idEstado");
            entity.Property(e => e.IdSeguimiento).HasMaxLength(100);
            entity.Property(e => e.IdmetodoPago).HasColumnName("IDMetodoPago");
            entity.Property(e => e.ImagenPersonalizada)
                .HasMaxLength(255)
                .HasColumnName("imagenPersonalizada");
            entity.Property(e => e.Notas).HasMaxLength(255);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Pedidos_Usuario");
        });

        modelBuilder.Entity<ProductoImagenes>(entity =>
        {
            entity.Property(e => e.Ruta).HasMaxLength(255);

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoImagenes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_ProductoImagenes_Productos");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.Property(e => e.ClasificacionEdad).HasMaxLength(20);
            entity.Property(e => e.Editorial).HasMaxLength(100);
            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.IdEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("idEstado");
            entity.Property(e => e.IdGeneroProducto).HasColumnName("idGeneroProducto");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PromedioValoracion).HasDefaultValue(0.0);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK_Productos_Autor");

            entity.HasOne(d => d.IdGeneroProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdGeneroProducto)
                .HasConstraintName("FK_Productos_GeneroProducto");

            entity.HasMany(d => d.Etiqueta).WithMany(p => p.Producto)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductoEtiquetas",
                    r => r.HasOne<Etiquetas>().WithMany()
                        .HasForeignKey("EtiquetaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductoEtiquetas_Etiquetas"),
                    l => l.HasOne<Productos>().WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductoEtiquetas_Productos"),
                    j =>
                    {
                        j.HasKey("ProductoId", "EtiquetaId");
                    });
        });

        modelBuilder.Entity<Promociones>(entity =>
        {
            entity.HasKey(e => e.IdPromocion);

            entity.Property(e => e.IdPromocion).HasColumnName("idPromocion");
            entity.Property(e => e.DescuentoPorcentaje).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IdEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("idEstado");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Promociones_Categoria");

            entity.HasOne(d => d.Producto).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Promociones_Producto");

            entity.HasOne(d => d.TipoPromocion).WithMany(p => p.Promociones)
                .HasForeignKey(d => d.TipoPromocionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Promociones_Tipo");
        });

        modelBuilder.Entity<Reseñas>(entity =>
        {
            entity.Property(e => e.Aprobada).HasDefaultValue(true);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reseñas_Pedido");

            entity.HasOne(d => d.Producto).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Reseñas_Producto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Reseñas_Usuario");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.Idrol);

            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.Descripcion).HasMaxLength(15);
        });

        modelBuilder.Entity<TipoPromocion>(entity =>
        {
            entity.HasKey(e => e.IdTipoPromocion);

            entity.Property(e => e.IdTipoPromocion).HasColumnName("idTipoPromocion");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A19F15EE2B8").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.IdEstado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("idEstado");
            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UltimoInicioSesion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.Idrol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

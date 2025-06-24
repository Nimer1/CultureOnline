using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CultureOnline.Application.DTOs
{
    public record  ProductoDTO
    {
        [Display(Name = "Identificador Libro")]
      //  [ValidateNever]
        public int Id { get; set; }

        [Display(Name = "Nombre Libro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        //[Range(0, 999999999, ErrorMessage = "El valor mínimo es {0}")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Precio { get; set; }

        [Display(Name = "Cantidad")]
        public int Stock { get; set; }

        [Display(Name = "Estado ")]
        public string? Estado { get; set; }

        [Display(Name = "Año de publicación")]
        public int? AnioPublicacion { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "{0} es un dato requerido")]

        public int? IdAutor { get; set; }

        [Display(Name = "Clasificación")]
        public string? ClasificacionEdad { get; set; }

        [Display(Name = "Editorial ")]
        public string? Editorial { get; set; }

        //[Display(Name = "Genero del Producto")]
        //public int? IdGeneroProducto { get; set; }

        //[Display(Name = "Categoría")]
        //public int CategoriaId { get; set; }


        [Display(Name = "Valoración")]
        public double? PromedioValoracion { get; set; }

        [Display(Name = "Personalizable")]
        public bool EsPersonalizable { get; set; }

        [Display(Name = "Autor")]
        // [ValidateNever]
        public AutorDTO? IdAutorNavigation { get; set; }
        public CategoriaDTO? Categoria { get; set; }

        //[ValidateNever]
        // public virtual List<PedidoDetalleDTO> DetallePedido { get; set; } = null!;
        public List<CategoriaDTO> Categorias { get; set; } = new();
        public List<EtiquetaDTO> Etiquetas { get; set; } = new();
        public List<ProductoImagenesDTO> Imagenes { get; set; } = new();
        public List<ReseñaDTO> Reseñas { get; set; } = new();
        public List<PromocionDTO> Promociones { get; set; } = new();
    }
}

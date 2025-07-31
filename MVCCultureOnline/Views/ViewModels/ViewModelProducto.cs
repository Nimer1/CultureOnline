using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCCultureOnline.Views.ViewModels
{
    public record ViewModelProducto : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [Display(Name = "Nombre del producto")]
        public string Nombre { get; set; } = default!;

        [Required(ErrorMessage = "La descripción del producto es obligatorio.")]
        [Display(Name = "Descripción del producto")]
        public string Descripcion { get; set; } = default!;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Display(Name = "Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad en stock debe ser al menos 1")]
        public int? Stock { get; set; }


        [Required(ErrorMessage = "Debe cargar al menos una imagen")]
        [Display(Name = "Imágenes")]
        //public IFormFile[] Imagenes { get; set; } = [];

        public List<string> Imagenes { get; set; } = new();

        [Required(ErrorMessage = "Debe seleccionar al menos una categoría")]
        [Display(Name = "Categorías")]
        public List<int> CategoriaIds { get; set; } = new();

        [Required(ErrorMessage = "Debe seleccionar al menos una etiqueta")]
        [Display(Name = "Etiquetas")]
        public List<int> EtiquetaIds { get; set; } = [];

        [Display(Name = "Promedio de Valoraciones")]
        public float PromedioValoracion { get; set; }

        //[Required(ErrorMessage = "El año de publicación es obligatorio.")]
        // Campos solo si es un libro
        [Display(Name = "Año de publicación")]
        public int? AnioPublicacion { get; set; }

        //[Required(ErrorMessage = "Debe seleccionar un autor.")]
        [Display(Name = "Autor")]
        public int? AutorId { get; set; }

        //[Required(ErrorMessage = "La clasificación de edad es obligatorio.")]
        [Display(Name = "Clasificación de Edad")]
        public string? ClasificacionEdad { get; set; }

        //[Required(ErrorMessage = "El nombre de la editorial es obligatorio.")]
        [Display(Name = "Editorial")]
        public string? Editorial { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de producto.")]
        [Display(Name = "Tipo de Producto")]
        public string? TipoProducto { get; set; }

        [Display(Name = "¿Es personalizable?")]
        public bool EsPersonalizable { get; set; }


        // Listas para los dropdowns
        public IEnumerable<SelectListItem> Categorias { get; set; } = [];
        public IEnumerable<SelectListItem> Etiquetas { get; set; } = [];
        public IEnumerable<SelectListItem> Autores { get; set; } = [];
        public IEnumerable<SelectListItem> TiposProducto { get; set; } = [];

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TipoProducto == "Libro")
            {
                if (!AnioPublicacion.HasValue)
                    yield return new ValidationResult("El año de publicación es obligatorio.", new[] { nameof(AnioPublicacion) });

                if (!AutorId.HasValue)
                    yield return new ValidationResult("Debe seleccionar un autor.", new[] { nameof(AutorId) });

                if (string.IsNullOrWhiteSpace(ClasificacionEdad))
                    yield return new ValidationResult("La clasificación de edad es obligatoria.", new[] { nameof(ClasificacionEdad) });

                if (string.IsNullOrWhiteSpace(Editorial))
                    yield return new ValidationResult("El nombre de la editorial es obligatorio.", new[] { nameof(Editorial) });
            }
        }
    }
}

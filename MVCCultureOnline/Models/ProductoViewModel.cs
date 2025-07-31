using CultureOnline.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCultureOnline.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public IFormFileCollection? Imagenes { get; set; }

        // Imágenes ya existentes que vienen de la BD
        public List<ProductoImagenes> ImagenesExistentes { get; set; } = new();

        // Ids de imágenes que el usuario quiere eliminar
        public List<int> ImagenesEliminadas { get; set; } = new();

        public int? CategoriaId { get; set; }
        public List<int>? EtiquetaIds { get; set; }

        public float PromedioValoracion { get; set; } = 0;

        // Solo si es libro
        public int? AnioPublicacion { get; set; }
        public int? AutorId { get; set; }
        public string? ClasificacionEdad { get; set; }
        public string? Editorial { get; set; }

        // Para SelectLists
        public IEnumerable<SelectListItem>? Categorias { get; set; }
        public IEnumerable<SelectListItem>? Etiquetas { get; set; }
        public IEnumerable<SelectListItem>? Autores { get; set; }
    }
}
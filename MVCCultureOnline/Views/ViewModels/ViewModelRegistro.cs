using System.ComponentModel.DataAnnotations;

namespace MVCCultureOnline.Views.ViewModels
{
    public record ViewModelRegistro
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Nombre { get; set; } = default!;

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        public string Correo { get; set; } = default!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = default!;

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; } = default!;
    }
}

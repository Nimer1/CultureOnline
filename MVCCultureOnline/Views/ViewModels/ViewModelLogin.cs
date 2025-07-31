using System.ComponentModel.DataAnnotations;

namespace MVCCultureOnline.Views.ViewModels
{
    public record ViewModelLogin
    {
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "{0} es requerido")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "El {0} debe tener entre {2} y {1} caracteres.")]
        [DataType(DataType.EmailAddress)]
        public string User { get; set; } = default!;
        [StringLength(15, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 15 caracteres.")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Solamente números y letras")]
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = default!;
    }
}

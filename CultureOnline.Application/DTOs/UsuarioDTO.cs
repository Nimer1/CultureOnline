using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Correo { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasena { get; set; } = null!;

        [Display(Name = "Rol")]
        public int IDRol { get; set; }

        [Display(Name = "Último inicio de sesión")]
        public DateTime? UltimoInicioSesion { get; set; }

        [Display(Name = "Estado")]
        public int? IdEstado { get; set; }

        public virtual RolUsuarioDTO IDRolNavigation { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureOnline.Application.DTOs
{
    public record RolUsuarioDTO
    {
        public int IDRol { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; } = null!;

        public virtual List<UsuarioDTO> Usuario { get; set; } = null!;
    }
}

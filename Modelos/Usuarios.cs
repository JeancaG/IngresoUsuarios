using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Usuarios
    {
        [Required(ErrorMessage = "El Campo Codigo es Obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El Campo Nombre es Obligatorio")]
        public string Rol { get; set; }
        public string Clave { get; set; }
        public bool EstaActivo { get; set; }
    }
}
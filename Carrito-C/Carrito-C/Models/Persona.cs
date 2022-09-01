using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.ErrorNombres)]
        public string Nombre { get; set; }
        [StringLength(100, MinimumLength = 2, ErrorMessage = MsgError.ErrorNombres)]
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }

        public string Email { get; set; }
        public DateOnly FechaAlta { get; set; }
    }
}

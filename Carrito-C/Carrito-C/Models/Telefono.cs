using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carrito_C.Models
{
    public class Telefono
    {
        [Key, ForeignKey("Cliente")]
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [RegularExpression(Validaciones.RegexTelefono, ErrorMessage = MsgError.Telefono)]
        [Display(Name = Alias.Telefono)]
        public int Numero { get; set; }
        public Cliente Cliente { get; set; }
    }
}

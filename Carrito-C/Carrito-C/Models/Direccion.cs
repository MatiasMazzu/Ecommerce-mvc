using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carrito_C.Models
{
    public class Direccion
    {
        [Key, ForeignKey("Cliente")]
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.DireccionMaxString, MinimumLength = Validaciones.DireccionMinString,
        ErrorMessage = MsgError.CommonError)]
        [Display(Name = Alias.Direccion)]
        public string Calle { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.NumeracionMin, Validaciones.NumeracionMax, ErrorMessage = MsgError.CommonError)]
        [Display(Name = Alias.Numeracion)]
        public int Numero { get; set; }
        public Cliente Cliente { get; set; }
    }
}

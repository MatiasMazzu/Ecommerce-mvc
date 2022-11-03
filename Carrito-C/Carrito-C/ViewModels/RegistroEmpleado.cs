using Carrito_C.Helpers;
using Carrito_C.Models;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.ViewModels
{
    public class RegistroEmpleado
    {
        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString,
        ErrorMessage = MsgError.CommonError)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.ApellidoMaxString, MinimumLength = Validaciones.ApellidoMinString,
        ErrorMessage = MsgError.CommonError)]
        public string Apellido { get; set; }

    }
}

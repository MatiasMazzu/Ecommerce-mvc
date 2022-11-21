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

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.DniMin, Validaciones.DniMax, ErrorMessage = MsgError.CommonError)]
        [Display(Name = Alias.Dni)]
        public int Dni { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.Telefono)]
        [RegularExpression(Validaciones.RegexTelefono, ErrorMessage = MsgError.Telefono)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.DireccionMaxString, MinimumLength = Validaciones.DireccionMinString, ErrorMessage = MsgError.UserName)]
        [Display(Name = Alias.Direccion)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = Alias.Mail)]
        public string Email { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.ConfirmPassword)]
        [Compare("Password", ErrorMessage = MsgError.PassMissMatch)]
        public string ConfirmacionPassword { get; set; }

        public string Rol { get; set; }
    }
}

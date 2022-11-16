using Carrito_C.Helpers;
using Carrito_C.Models;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.ViewModels
{
    public class RegistroUsuario
    {
        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString,
            ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.ApellidoMaxString, MinimumLength = Validaciones.ApellidoMinString,
        ErrorMessage = MsgError.CommonError)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.DniMin, Validaciones.DniMax, ErrorMessage = MsgError.CommonError)]
        [Display(Name = Alias.Dni)]
        public int Dni { get; set; }

        public Telefono Telefono { get; set; }

        public Direccion Direccion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = Alias.Mail)]
        public  string Email { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.ConfirmPassword)]
        [Compare("Password", ErrorMessage = MsgError.PassMissMatch)]
        public string ConfirmacionPassword { get; set; }
        
    }
}

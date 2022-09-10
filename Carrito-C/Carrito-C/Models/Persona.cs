using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.ApellidoMaxString, MinimumLength = Validaciones.ApellidoMinString, ErrorMessage = MsgError.CommonError)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.TelefonoMinRange, Validaciones.TelefonoMaxRange, ErrorMessage = MsgError.CommonError2)]
        public int Telefono { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.DireccionMaxString, MinimumLength = Validaciones.DireccionMinString, ErrorMessage = MsgError.CommonError)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.UserNameMaxString, MinimumLength = Validaciones.UserNameMinString, ErrorMessage = MsgError.UserName)]
        public string UserName { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.PasswordMaxString, MinimumLength = Validaciones.PasswordMinString, ErrorMessage = MsgError.Password)]
        public string Password { get; set; }
    }
}

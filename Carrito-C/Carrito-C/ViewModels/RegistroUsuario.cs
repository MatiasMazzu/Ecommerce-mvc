using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Carrito_C.ViewModels
{
    public class RegistroUsuario
    {
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

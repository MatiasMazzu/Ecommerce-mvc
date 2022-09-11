using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(Validaciones.DireccionMaxString, MinimumLength = Validaciones.DireccionMinString, ErrorMessage = MsgError.CommonError)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), Range(Validaciones.TelefonoMinRange, Validaciones.TelefonoMaxRange, ErrorMessage = MsgError.CommonError2)]
        public int Telefono { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = "Correo electronico")]
        public string Email {  get; set; }
        List<StockItem> StockItems { get; set; }

    }
}

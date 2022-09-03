using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), Range(100000, 99999999, ErrorMessage = MsgError.CommonError2)]
        public int Telefono { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        [EmailAddress(ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = "Correo electronico")]
        public string Email {  get; set; }
        List<StockItem> StockItems { get; set; }

    }
}

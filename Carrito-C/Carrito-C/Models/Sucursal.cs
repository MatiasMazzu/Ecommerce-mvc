using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Sucursal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString, 
            ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public Telefono Telefono { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.EmailAddress, ErrorMessage = MsgError.MsgEmail)]
        [Display(Name = Alias.Mail)]
        public string Email {  get; set; }

        public List<StockItem> ProductosSucursal { get; set; }

    }
}

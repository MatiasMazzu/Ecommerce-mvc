using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class CarritoItem
    {
        public CarritoItem()
        {

        }

        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.CarritoId)]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.ProductoId)]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Currency)]
        public int ValorUnitario { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.CantidadMinInt, Validaciones.CantidadMaxInt, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }

        
        [DataType(DataType.Currency)]
        public int Subtotal { get; set; }

    }        
 
}

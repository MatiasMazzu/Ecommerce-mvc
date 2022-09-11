using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class CarritoItem
    {

        public int Id { get; set; }

        public Carrito Carrito { get; set; }

        public Producto Producto { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(1, 199999999, ErrorMessage = MsgError.CommonError2)]
        public int ValorUnitario { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(1, 199999999, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(1, 199999999, ErrorMessage = MsgError.CommonError2)]
        public int Subtotal { get; set; }

    }        
 
}


using System.ComponentModel.DataAnnotations;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Carrito
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        public Boolean Activo { get; set; }
        public Cliente Cliente { get; set; }
        public List <CarritoItem> CarritoItems{ get; set; }

        public List<Producto> Productos { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(1, 199999999, ErrorMessage = MsgError.CommonError2)]
        public int Subtotal { get; set; }
    }
}

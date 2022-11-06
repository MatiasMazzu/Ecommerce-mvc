
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Carrito
    {

        [Key, ForeignKey("Cliente")]
        public int Id { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        public bool Activo { get; set; }

        public Cliente Cliente { get; set; }
        public List <CarritoItem> CarritoItems{ get; set; }

        [DataType(DataType.Currency)]
        public int Subtotal { get; set; }
    }
}

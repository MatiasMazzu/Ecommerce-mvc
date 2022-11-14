
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Carrito
    {
        public int Id { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        public bool Activo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List <CarritoItem> CarritoItems{ get; set; }

        [DataType(DataType.Currency)]
        public int Subtotal { get; set; }
        public void SetSubtotal()
        {
            Subtotal = 0;
            foreach (CarritoItem item in CarritoItems)
            {
                Subtotal += item.Subtotal;
            }

        }
    }
}

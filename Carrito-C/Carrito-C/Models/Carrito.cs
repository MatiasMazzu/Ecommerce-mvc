
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

        [NotMapped]
        [DataType(DataType.Currency)]
        public double Total
        {
            get
            {
                double total = 0;
                if(CarritoItems != null)
                {
                    foreach(CarritoItem item in CarritoItems)
                    {
                        total += item.Subtotal;
                    }
                }
                return total;
            }
        }

    }
}

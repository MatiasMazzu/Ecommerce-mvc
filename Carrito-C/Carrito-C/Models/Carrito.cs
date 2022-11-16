
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

        private double Helper = 0;

        [NotMapped]
        [DataType(DataType.Currency)]
        public double Subtotal
        {
            get
            {
                Helper = 0;
                if (CarritoItems != null)
                {
                    foreach (CarritoItem item in CarritoItems)
                    {
                        Helper += item.Subtotal;
                    }
                }
                return Helper;
            }
        }

    }
}

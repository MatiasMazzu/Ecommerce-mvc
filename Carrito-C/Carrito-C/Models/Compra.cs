using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Carrito_C.Models
{
    public class Compra
    {

        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.ClienteId)]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.CarritoId)]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [DataType(DataType.Currency)]
        public double Total
        {
            get
            {
                double Total = 0;
                foreach (ComprasItem item in ComprasItems)
                {
                    Total += item.Subtotal;
                }
                return Total;
            }
        }

        public List<ComprasItem> ComprasItems { get; set; }
        
    }
}

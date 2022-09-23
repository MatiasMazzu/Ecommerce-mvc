
using System.ComponentModel.DataAnnotations;
using Carrito_C.Helpers;

namespace Carrito_C.Models
{
    public class Carrito
    {
        public int Id { get; set; }

        public Carrito(int ClienteId)
        {
            this.Activo = true;
            this.ClienteId = ClienteId;
            this.CarritoItems = new List<CarritoItem>();
        }

        [Required(ErrorMessage = MsgError.Requerido)]
        public bool Activo { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Key]
        [Display(Name = Alias.ClienteId)]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List <CarritoItem> CarritoItems{ get; set; }

        [DataType(DataType.Currency)]
        public int Subtotal { get; set; }
    }
}

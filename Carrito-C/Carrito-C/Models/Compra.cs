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
        public double Total{get; set; }

        public List<ComprasItem> ComprasItems { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Date)]
        [Display(Name = Alias.FechaDeCompra)]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public int SucursalId { get; set; }

        public Sucursal Sucursal { get; set; }

    }
}

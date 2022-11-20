using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Carrito_C.Models
{
    public class ComprasItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = "Compras")]
        public int CompraId { get; set; }
        public Compra Compra { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.ProductoId)]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.CantidadMinInt, Validaciones.CantidadMaxInt, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }


        [DataType(DataType.Currency)]
        public double Subtotal { get; set; }
    }
}
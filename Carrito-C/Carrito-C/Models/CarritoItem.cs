using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carrito_C.Models
{
    public class CarritoItem
    {
        public CarritoItem(){}

        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.CarritoId)]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = Alias.ProductoId)]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(Validaciones.CantidadMinInt, Validaciones.CantidadMaxInt, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.Currency)]
        public double ValorUnitario { 
            get 
            {
                double valor = 0;
                if (Producto != null)
                {
                    valor = Producto.PrecioVigente;
                }
                return valor;
            } 
        }

        [NotMapped]
        [DataType(DataType.Currency)]
        public double Subtotal
        {
            get
            {
                return Cantidad * ValorUnitario;
            }
        }

    }        
 
}

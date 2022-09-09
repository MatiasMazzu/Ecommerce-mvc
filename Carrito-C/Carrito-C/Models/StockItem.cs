using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class StockItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        public int ProductoId { get; set; } 
        public Producto Producto { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        public int SucursalId { get; set; } 
        public Sucursal Sucursal { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [Range(1, 199999999, ErrorMessage = MsgError.CommonError2)]
        public int Cantidad { get; set; }


    }
}

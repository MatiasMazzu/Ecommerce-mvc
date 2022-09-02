using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public List<Producto> Productos { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public int Cantidad { get; set; }
    }
}

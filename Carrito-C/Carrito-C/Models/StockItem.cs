using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class StockItem
    {

        public List<Producto> Productos { get; set; }
        public List<Sucursal> Sucursales { get; set; }
        public int Cantidad { get; set; }
    }
}

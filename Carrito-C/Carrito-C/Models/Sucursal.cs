using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email {  get; set; }
        List<StockItem> StockItems { get; set; }

    }
}

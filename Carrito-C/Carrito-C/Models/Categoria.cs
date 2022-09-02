using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = MsgError.Requerido)]
        [MaxLength(50, ErrorMessage = MsgError.MsgMaxStr)]
        [MinLength(2, ErrorMessage = MsgError.MsgMinStr)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        List<Producto> Productos;
    }

}

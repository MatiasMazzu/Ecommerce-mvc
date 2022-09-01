using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [StringLength(50,MinimumLength =2, ErrorMessage=MsgError.ErrorNombres)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        List<Producto> Productos;
    }

}

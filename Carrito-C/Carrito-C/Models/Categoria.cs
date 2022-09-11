using Carrito_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Carrito_C.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(Validaciones.NombreMaxString, MinimumLength = Validaciones.NombreMinString, ErrorMessage = MsgError.CommonError)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = MsgError.Requerido), StringLength(Validaciones.DescripcionMaxString, MinimumLength = Validaciones.DireccionMinString, ErrorMessage = MsgError.CommonError)]
        public string Descripcion { get; set; }
        List<Producto> Productos;
    }

}
